using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace vosplzen.sem2._2023.apiClient
{
    public class ApiClient : IDisposable
    {
        private readonly HttpClient httpClient;

        public ApiClient()
        {
            // Vytvoření HttpClientHandler s vlastním callbackem pro validaci certifikátu
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Předání handleru do HttpClient
            httpClient = new HttpClient(clientHandler);
        }

        public async Task<bool> SendMessage(object message, string baseurl, string authToken)
        {
            try
            {
                var settings = new JsonSerializerSettings { Culture = new CultureInfo("cs-CZ") };
                var json = JsonConvert.SerializeObject(message, Formatting.Indented, settings);

                Console.WriteLine("message:");
                Console.WriteLine(json);

                var data = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Clear();

                // Nastavování hlaviček na HttpContent
                data.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // Nastavení Authorization hlavičky
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authToken);

                var response = await httpClient.PostAsync(baseurl, data);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Message sent successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return false;
        }

        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}
