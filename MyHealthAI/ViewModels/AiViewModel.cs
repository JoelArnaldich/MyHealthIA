using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MyHealthAI.Models;
using Newtonsoft.Json;

public class AiViewModel : BaseViewModel
{
    private string _userInput;
    private readonly MyHealthAI.Services.GeminiClient geminiClient; // Cliente para interactuar con Gemini
    public ObservableCollection<ChatMessage> Messages { get; set; }
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



    // Constructor que recibe el cliente personalizado GeminiClient
    public AiViewModel(MyHealthAI.Services.GeminiClient geminiClient)
    {
        this.geminiClient = geminiClient;
        Messages = new ObservableCollection<ChatMessage>();
        SendMessageCommand = new RelayCommand(async (param) => await SendMessage());
    }

    private async Task SendMessage()
    {
        // Verifica si UserInput no es nulo ni vacío
        if (string.IsNullOrEmpty(UserInput))
        {
            return; // No hacer nada si el mensaje está vacío
        }

        // Agrega el mensaje del usuario al chat (en el hilo principal)
        Application.Current.Dispatcher.Invoke(() =>
        {
            Messages.Add(new ChatMessage
            {
                Sender = "User",
                Message = UserInput,
                IsUserMessage = true
            });
        });

        // Limpia el campo de entrada
        string userMessage = UserInput;
        UserInput = string.Empty;
        OnPropertyChanged(nameof(UserInput));

        try
        {
            // Llama al cliente Gemini para obtener la respuesta
            var responseJson = await geminiClient.TextPrompt(userMessage);
            Console.WriteLine("Respuesta JSON: " + responseJson);

            // Deserializa la respuesta JSON en un objeto GeminiResponse
            var response = JsonConvert.DeserializeObject<GeminiResponse>(responseJson);

            // Verifica si hay contenido en los candidatos y agrega al chat
            if (response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Add(new ChatMessage
                    {
                        Sender = "Gemini",
                        Message = response.Candidates.FirstOrDefault().Content.Parts.FirstOrDefault().Text,
                        IsUserMessage = false
                    });
                });
            }
            else
            {
                // Si no hay texto en la respuesta
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
                    IsUserMessage = false  // Es un mensaje del usuario
                });
            });
        }

    }
}
