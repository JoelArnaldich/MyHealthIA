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
    private readonly GeminiClient geminiClient;
    private readonly AppDbContext _dbContext;
    private string context; // Contexto acumulado para el informe

    public ObservableCollection<ChatMessage> Messages
    {
        get { return ConversationHistory.Instance.Messages; }
    }

    public RelayCommand GenerateReportCommand { get; set; }

    public AiViewModel(GeminiClient geminiClient, AppDbContext dbContext)
    {
        this.geminiClient = geminiClient;
        this._dbContext = dbContext;

        // Configurar el comando para generar el informe
        GenerateReportCommand = new RelayCommand(async (param) => await GenerateReport());
    }
    private async Task GenerateReport()
    {
        try
        {
            // Agregar mensaje inicial en el hilo principal
            Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(new ChatMessage
                {
                    Sender = "MyHealthAI",
                    Message = "Generando informe..."
                });
            });

            // Obtener datos del usuario por ID
            string userData = await GetUserDataByIdAsync(CurrentUser.LoggedInUserId);

            if (string.IsNullOrEmpty(userData))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Add(new ChatMessage
                    {
                        Sender = "MyHealthAI",
                        Message = "No se pudo obtener la información del usuario para generar el informe."
                    });
                });
                return;
            }

            int? userObjective = await GetUserObjectiveIdAsync(CurrentUser.LoggedInUserId);
            String userObjectiveS = " ";
            if (userObjective == 1)
                userObjectiveS = "El objetivo del usuario es peder peso";

            else if (userObjective == 2)
                userObjectiveS = "El objetivo del usuario es ganar peso i musculo";

            else if (userObjective == 3)
                userObjectiveS = "El objetivo del usuario Perder peso y ganar musculo";

            else if (userObjective == 4)
                userObjectiveS = "El objetivo del usuario es manerse igual";


            context = "Eres una inteligencia artificial diseñada para generar informes nutricionales personalizados. Tu objetivo es analizar los datos del usuario y proporcionar un informe detallado sobre su estado nutricional y físico. Aquí tienes los datos del usuario:\n";
            context += userObjectiveS;
            context += userData;


            var responseJson = await geminiClient.TextPrompt(context);

            if (string.IsNullOrEmpty(responseJson))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Add(new ChatMessage
                    {
                        Sender = "MyHealthAI",
                        Message = "No se recibió una respuesta válida de la API."
                    });
                });
                return;
            }

            var response = JsonConvert.DeserializeObject<GeminiResponse>(responseJson);

            string report = response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;
            if (!string.IsNullOrEmpty(report))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Add(new ChatMessage
                    {
                        Sender = "Gemini",
                        Message = report
                    });
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Add(new ChatMessage
                    {
                        Sender = "MyHealthAI",
                        Message = "La IA no generó un informe válido."
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
                    Sender = "MyHealthAI",
                    Message = $"Error al generar el informe: {ex.Message}"
                });
            });
        }
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
        var meals = await _dbContext.Meals.Where(m => m.UserID == userId).OrderBy(m => m.MealDate).ToListAsync();
        var exercises = await _dbContext.Exercises.Where(e => e.UserID == userId).OrderBy(e => e.Date).ToListAsync();

        if (meals.Any())
        {
            sb.AppendLine("Comidas y actividad diaria:");
            var groupedMeals = meals.GroupBy(m => m.MealDate); // Agrupar comidas por DateOnly

            foreach (var mealGroup in groupedMeals)
            {

                var date = mealGroup.Key; // `DateOnly` de la fecha actual del grupo
                sb.AppendLine($"Fecha: {date:yyyy-MM-dd}");

                // Listar las comidas para esta fecha
                sb.AppendLine("  Comidas:");
                foreach (var meal in mealGroup)
                {
                    sb.AppendLine($"    - {meal.Name}: {meal.Kcal} kcal, {meal.Protein}g proteína, {meal.Carbohydrate}g carbohidrato, {meal.Fat}g grasa");
                }

                // Verificar si hay ejercicios para esta fecha
                var exercisesForDate = exercises.Where(e => e.Date == date).ToList();
                if (exercisesForDate.Any())
                {
                    sb.AppendLine("  Ejercicio:");
                    foreach (var exercise in exercisesForDate)
                    {
                        sb.AppendLine($"    - {exercise.ExerciseType}: {exercise.DurationInMinutes} min, {exercise.CaloriesBurned} kcal quemadas");
                    }
                }
                else
                {
                    sb.AppendLine("  Ejercicio: No se registraron ejercicios para este día.");
                }
            }
        }
        else
        {
            sb.AppendLine("No se encontraron comidas registradas.");
        }

        // Agua del usuario
        var water = await _dbContext.Water.Where(w => w.UserID == userId).OrderBy(w => w.Date).ToListAsync();
        if (water.Any())
        {
            sb.AppendLine("Consumo de agua:");
            foreach (var waterRecord in water)
            {
                sb.AppendLine($"- {waterRecord.Date:yyyy-MM-dd}: {waterRecord.WaterMl} ml de agua");
            }
        }
        else
        {
            sb.AppendLine("No se encontraron registros de agua.");
        }

        return sb.ToString();
    }




    public async Task<int?> GetUserObjectiveIdAsync(int userId)
    {
        // Verificar si el ID del usuario es válido
        if (userId <= 0)
        {
            return null; // ID inválido
        }

        // Obtener el usuario y su ObjectiveID
        var user = await _dbContext.Users
            .Where(u => u.ID == userId)
            .Select(u => u.ObjectiveID) // Selecciona solo el campo ObjectiveID
            .FirstOrDefaultAsync();

        return user; // Esto devolverá el ID del objetivo del usuario, o null si no lo encuentra
    }
}

