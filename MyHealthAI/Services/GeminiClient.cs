using System.Net.Http;
using System.Text;


namespace MyHealthAI.Services
{
    public class GeminiClient
    {
        private static readonly string apiKey = "AIzaSyDNCLxm1nKjNlvBOtuwfPpx0TyamREswPE";
        private static readonly string apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key={apiKey}";

        public async Task<string> TextPrompt(string inputText)
        {
            using (HttpClient client = new HttpClient())
            {

                var jsonData = $"{{\"contents\":[{{\"parts\":[{{\"text\":\"{inputText}\"}}]}}]}}";
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                try
                {

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);


                    response.EnsureSuccessStatusCode();


                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                catch (HttpRequestException e)
                {

                    throw new Exception($"Error en la solicitud HTTP: {e.Message}. Stack Trace: {e.StackTrace}");
                }
                catch (Exception e)
                {

                    throw new Exception($"Ocurrió un error: {e.Message}");
                }
            }
        }
    }
}
