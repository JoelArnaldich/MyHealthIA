using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using MyHealthAI.Models;
using MyHealthAI.Services;
using Newtonsoft.Json;

public class AiViewModel : BaseViewModel
{
    private string _userInput;
    private readonly GeminiClient geminiClient;
    private readonly AppDbContext _dbContext;
    private string context;  // Guardar el contexto acumulado de la conversación

    public ObservableCollection<ChatMessage> Messages
    {
        get { return ConversationHistory.Instance.Messages; }
    }

    public RelayCommand SendMessageCommand { get; set; }

    public string UserInput
    {
        get { return _userInput; }
        set
        {
            _userInput = value;
            OnPropertyChanged();
        }
    }

    public AiViewModel(GeminiClient geminiClient, AppDbContext dbContext)
    {
        this.geminiClient = geminiClient;
        this._dbContext = dbContext;

        SendMessageCommand = new RelayCommand(async (param) => await SendMessage());

        // Llamamos a SendContext en el constructor para obtener y construir el contexto.
        Task.Run(async () => await SendContext());
    }

    private async Task SendContext()
    {
        // Obtener datos del usuario por ID
        string userData = await GetUserDataByIdAsync(CurrentUser.LoggedInUserId);

        if (string.IsNullOrEmpty(userData))
        {
            // Si no se obtienen datos del usuario, muestra un mensaje y no los agrega al contexto
            context = "No se pudo obtener la información del usuario.";
        }
        else
        {
            // Concatenar el contexto de la inteligencia artificial con los datos del usuario
            context = "Eres una inteligencia artificial diseñada para actuar como un nutricionista virtual. Tu función es proporcionar consejos personalizados al usuario sobre su proceso de cambio físico, basándote en datos sobre su dieta, ejercicio, hábitos y otros factores relevantes. Tu objetivo es ofrecer guías prácticas y recomendaciones que ayuden al usuario a mantenerse enfocado y motivado en su meta de bienestar. Responde de forma clara, empática y constructiva, destacando los aspectos positivos y proporcionando sugerencias prácticas para mejorar. El usuario sabe que debe seguir las recomendaciones de un nutricionista humano para decisiones finales, por lo que tú solo sirves como una herramienta de apoyo en su camino hacia una mejor salud. Datos: ";
            context += userData;  // Añadir los datos obtenidos al contexto
        }

        // Enviar el contexto a la API sin mostrarlo en la interfaz
        await geminiClient.TextPrompt(BuildLog(string.Empty));  // Enviar solo el contexto inicial a la API
    }

    private async Task SendMessage()
    {
        if (string.IsNullOrEmpty(UserInput))
        {
            return;
        }

        // Agregar el mensaje del usuario a la vista
        Application.Current.Dispatcher.Invoke(() =>
        {
            Messages.Add(new ChatMessage
            {
                Sender = "User",
                Message = UserInput,
            });
        });

        string userMessage = UserInput;
        UserInput = string.Empty;
        OnPropertyChanged(nameof(UserInput));

        try
        {
            // Aquí agregamos el contexto acumulado más el mensaje actual del usuario
            context += $"\nUser: {userMessage}";  // Añadir el mensaje del usuario al contexto

            // Ahora enviamos el contexto completo (acumulado) a la API
            var responseJson = await geminiClient.TextPrompt(BuildLog(userMessage));

            var response = JsonConvert.DeserializeObject<GeminiResponse>(responseJson);

            // Si la respuesta contiene un texto, la mostramos, de lo contrario mostramos un mensaje de error
            if (response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Add(new ChatMessage
                    {
                        Sender = "Gemini",
                        Message = response.Candidates.FirstOrDefault().Content.Parts.FirstOrDefault().Text,
                    });
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Add(new ChatMessage
                    {
                        Sender = "System",
                        Message = "La respuesta de Gemini está vacía o no contiene texto.",
                    });
                });
            }
        }
        catch (Exception ex)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(new ChatMessage
                {
                    Sender = "System",
                    Message = $"Error: {ex.Message}",
                });
            });
        }
    }

    private string BuildLog(string currentMessage)
    {
        // Aquí se construye el log con el contexto acumulado, pero no lo mostramos al usuario en la interfaz
        var log = string.Join("\n", Messages
            .Where(m => m.Sender != "System")  // No incluir los mensajes del sistema en el log
            .Select(m => $"{m.Sender}: {m.Message}"));

        // Añadimos el contexto al log para enviarlo a la API
        log = $"{context}\n{log}";  // El contexto acumulado se pone antes de los mensajes del chat

        return $"{log}\nUser: {currentMessage}";  // Añadimos el mensaje actual del usuario
    }

    private async Task<string> GetUserDataByIdAsync(int userId)
    {
        // Verificar si el ID del usuario es válido
        if (userId <= 0)
        {
            return "ID de usuario inválido.";
        }

        // Obtener los datos del usuario y las tablas relacionadas
        var user = await _dbContext.Users
            .Where(u => u.ID == userId)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return "No se encontraron datos para este usuario.";
        }

        // Construir el string con los datos del usuario
        StringBuilder sb = new StringBuilder();

        // Datos del usuario
        sb.AppendLine($"Usuario: {user.Name}");
        sb.AppendLine($"Edad: {user.Age}");
        sb.AppendLine($"Peso: {user.Weight} kg");
        sb.AppendLine($"Altura: {user.Height} cm");

        // Comidas del usuario
        var meals = await _dbContext.Meals.Where(m => m.UserID == userId).ToListAsync();
        if (meals.Any())
        {
            sb.AppendLine("Comidas:");
            foreach (var meal in meals)
            {
                sb.AppendLine($"- {meal.Name}: {meal.Kcal} kcal, {meal.Protein}g proteína, {meal.Carbohydrate}g carbohidrato, {meal.Fat}g grasa");
            }
        }
        else
        {
            sb.AppendLine("No se encontraron comidas registradas.");
        }

        // Ejercicio del usuario
        var exercises = await _dbContext.Exercises.Where(e => e.UserID == userId).ToListAsync();
        if (exercises.Any())
        {
            sb.AppendLine("Ejercicio:");
            foreach (var exercise in exercises)
            {
                sb.AppendLine($"- {exercise.ExerciseType}: {exercise.DurationInMinutes} min, {exercise.CaloriesBurned} kcal quemadas");
            }
        }
        else
        {
            sb.AppendLine("No se encontraron registros de ejercicios.");
        }

        // Agua del usuario
        var water = await _dbContext.Water.Where(w => w.UserID == userId).ToListAsync();
        if (water.Any())
        {
            sb.AppendLine("Agua:");
            foreach (var waterRecord in water)
            {
                sb.AppendLine($"- {waterRecord.Date}: {waterRecord.WaterMl} ml de agua");
            }
        }
        else
        {
            sb.AppendLine("No se encontraron registros de agua.");
        }

        return sb.ToString();
    }
}
