using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public interface Api
    {
        // API from BACKEND of My Project -> https://gitlab.com/niraj.mhrjn770/family-tree
        public const string BASE_URL = "http://127.0.0.1:8080/api/";
    }

    public class LoginRequest
    {
        [JsonPropertyName("username")] // For System.Text.Json
        public required string Username { get; set; }

        [JsonPropertyName("password")]
        public required string Password { get; set; }
    }

    public class Auth
    {
        private const string API = Api.BASE_URL + "user/";
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var loginData = new LoginRequest { Username = username, Password = password };

                var content = new StringContent(
                    JsonSerializer.Serialize(loginData),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync($"{API}admin_login/", content);
                Console.WriteLine(response.IsSuccessStatusCode);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Handle/log exception
                Console.WriteLine("Error!> " + ex.Message);
                return false;
            }
        }

        public static async Task<bool> IsConnected()
        {
            try
            {
                // 1. Check if network is theoretically available
                if (!NetworkInterface.GetIsNetworkAvailable())
                    return false;

                // 2. Quick ping check (uses ICMP)
                try
                {
                    using var ping = new Ping();
                    var reply = await ping.SendPingAsync(new Uri(Api.BASE_URL).Host, 1500);
                    if (reply.Status != IPStatus.Success)
                        return false;
                }
                catch (PingException)
                {
                    // Some networks block ICMP, so we'll proceed to HTTP check
                }

                // 3. Actual HTTP request to our API endpoint
                using var request = new HttpRequestMessage(
                    HttpMethod.Head, // Lightweight HEAD request
                    $"{Api.BASE_URL}"
                ); // Assume health check endpoint

                request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
                {
                    NoCache = true,
                };

                using var response = await _httpClient.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead
                ); // Don't buffer entire response

                return response.IsSuccessStatusCode
                    || response.StatusCode.Equals(System.Net.HttpStatusCode.Forbidden);
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("No connection"))
            {
                return false;
            }
            catch (TaskCanceledException) // Timeout
            {
                return false;
            }
            catch
            {
                return false; // Conservative approach - assume offline
            }
        }
    }
}
