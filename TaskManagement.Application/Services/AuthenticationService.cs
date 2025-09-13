using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using System.Text.Json.Serialization;

namespace TaskManagement.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            // Obtener configuración desde variables de entorno
            var auth0Domain = Environment.GetEnvironmentVariable("AUTH0_DOMAIN");
            var clientId = Environment.GetEnvironmentVariable("AUTH0_CLIENT_ID");
            var clientSecret = Environment.GetEnvironmentVariable("AUTH0_CLIENT_SECRET");
            var audience = Environment.GetEnvironmentVariable("AUTH0_AUDIENCE");

            // Validar que todos los valores requeridos estén presentes
            if (string.IsNullOrWhiteSpace(auth0Domain) || string.IsNullOrWhiteSpace(clientId) ||
                string.IsNullOrWhiteSpace(clientSecret) || string.IsNullOrWhiteSpace(audience))
            {
                throw new InvalidOperationException("Configuración de Auth0 incompleta. Verifique que las variables de entorno AUTH0_DOMAIN, AUTH0_CLIENT_ID, AUTH0_CLIENT_SECRET y AUTH0_AUDIENCE estén configuradas.");
            }

            // Validar que el dominio tenga el formato correcto
            if (!auth0Domain.Contains("."))
            {
                throw new InvalidOperationException($"Formato de dominio Auth0 inválido: {auth0Domain}");
            }

        var requestBody = new
        {
            client_id = clientId,
            client_secret = clientSecret,
            audience = audience,
            grant_type = "client_credentials"
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"https://{auth0Domain}/oauth/token", jsonContent);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener el token: {response.StatusCode}, Detalles: {errorContent}");
        }

        var responseString = await response.Content.ReadAsStringAsync();

            var responseObject = JsonSerializer.Deserialize<Auth0TokenResponse>(responseString);
            return responseObject?.AccessToken ?? string.Empty;
        }
    }

    public class Auth0TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = string.Empty;

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
