using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyApp.Services
{
    class UserData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("is_superuser")]
        public bool IsAdmin { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
    }

    public class User
    {
        private readonly string API = IApi.BASE_URL + "user/";

        private static readonly HttpClient _httpClient = new(
            new SocketsHttpHandler() { PooledConnectionLifetime = TimeSpan.FromMinutes(5) }
        );

        internal async Task<int> GetUserCount()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStreamAsync();
                var users = JsonSerializer.Deserialize<List<UserData>>(content);

                return users?.Count(user => !(user.IsAdmin)) ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR! " + ex.Message);
                return -1;
            }
        }

        internal async Task<int> GetUserId(string username)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API}");
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStreamAsync();
                var users = JsonSerializer.Deserialize<List<UserData>>(responseContent);
                var user = users?.FirstOrDefault(u => u.Username == username);
                if (user is null)
                    return 0;
                else
                    return user.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error  > parsing JSON: " + ex.Message);
                Console.WriteLine("Extra> " + ex.Source, ex.StackTrace);
                return -1;
            }
        }

        internal async Task<int> GetActiveUserCount()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStreamAsync();
                var users = JsonSerializer.Deserialize<List<UserData>>(content);

                return users?.Count(user => user.IsActive && !user.IsAdmin) ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR! " + ex.Message);
                return -1;
            }
        }
    }
}
