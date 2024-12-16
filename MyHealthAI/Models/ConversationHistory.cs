using System.Collections.ObjectModel;
using MyHealthAI.Models;

public class ConversationHistory
{
    // Instancia única del singleton
    private static ConversationHistory _instance;

    // Lista observable que almacena los mensajes
    public ObservableCollection<ChatMessage> Messages { get; private set; }

    // Constructor privado para evitar instancias múltiples
    private ConversationHistory()
    {
        Messages = new ObservableCollection<ChatMessage>();
    }

    // Propiedad para acceder a la instancia única
    public static ConversationHistory Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ConversationHistory();
            return _instance;
        }
    }
}
