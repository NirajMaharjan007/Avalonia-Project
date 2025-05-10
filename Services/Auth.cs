using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public interface Api
    {
        public const string BASE_URL = "http://127.0.0.1:8080/api/";
    }

    public class Auth
    {
        private const string API = Api.BASE_URL + "user/";
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var loginData = new { email = email, password = password };
                Console.WriteLine(loginData);
                var response = await _httpClient.PostAsJsonAsync($"{API}login/", loginData);
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
