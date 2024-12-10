using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyHealthAI.Services
{
    public class GeminiClient
    {
        private static readonly string apiKey = "";
        private static readonly string apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key={apiKey}";

        public async Task<string> TextPrompt(string inputText)
        {
            using (HttpClient client = new HttpClient())
            {
                // Crear el contenido de la solicitud en formato JSON
                var jsonData = $"{{\"contents\":[{{\"parts\":[{{\"text\":\"{inputText}\"}}]}}]}}";
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                try
                {
                    // Realizar la solicitud POST
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Lanzar una excepción si el estado no es exitoso
                    response.EnsureSuccessStatusCode();

                    // Leer y devolver la respuesta
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                catch (HttpRequestException e)
                {
                    // Manejo de excepciones específicas de HTTP
                    throw new Exception($"Error en la solicitud HTTP: {e.Message}");
                }
                catch (Exception e)
                {
                    // Manejo de otras excepciones
                    throw new Exception($"Ocurrió un error: {e.Message}");
                }
            }
        }
    }
}
