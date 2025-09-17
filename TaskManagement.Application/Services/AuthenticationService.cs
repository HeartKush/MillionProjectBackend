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
        private readonly string _auth0Domain;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _audience;

        public AuthenticationService(HttpClient httpClient, string auth0Domain, string clientId, string clientSecret, string audience)
        {
            _httpClient = httpClient;
            _auth0Domain = auth0Domain;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _audience = audience;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (string.IsNullOrWhiteSpace(_auth0Domain) || string.IsNullOrWhiteSpace(_clientId) ||
                string.IsNullOrWhiteSpace(_clientSecret) || string.IsNullOrWhiteSpace(_audience))
            {
                throw new InvalidOperationException("Configuración de Auth0 incompleta. Verifique que las variables de entorno AUTH0_DOMAIN, AUTH0_CLIENT_ID, AUTH0_CLIENT_SECRET y AUTH0_AUDIENCE estén configuradas.");
            }

            if (!_auth0Domain.Contains("."))
            {
                throw new InvalidOperationException($"Formato de dominio Auth0 inválido: {_auth0Domain}");
            }

        var requestBody = new
        {
            client_id = _clientId,
            client_secret = _clientSecret,
            audience = _audience,
            grant_type = "client_credentials"
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"https://{_auth0Domain}/oauth/token", jsonContent);

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
