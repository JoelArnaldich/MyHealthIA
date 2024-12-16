namespace MyHealthAI.Models
{
    public class ChatMessage
    {
        public string Sender { get; set; } // "User" o "Gemini"
        public string Message { get; set; }


        // Sobrescribe ToString para mostrar los atributos relevantes
        public override string ToString()
        {
            return $"{Sender}: {Message}";
        }
    }
}
