namespace MyHealthAI.Models
{
    public class ChatMessage
    {
        public string Sender { get; set; } // "User" o "Gemini"
        public string Message { get; set; }
        public bool IsUserMessage { get; set; } // True si es un mensaje del usuario, false si es de la IA

        // Sobrescribe ToString para mostrar los atributos relevantes
        public override string ToString()
        {
            return $"{Sender}: {Message}";
        }
    }
}
