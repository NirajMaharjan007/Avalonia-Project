using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public class UserData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("is_superuser")]
        public bool IsAdmin { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("date_joined")]
        public string? CreatedOn { get; set; }
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

        internal async Task<UserData> GetUser(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API}{id}/");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStreamAsync();
                var users = JsonSerializer.Deserialize<UserData>(content);
                return users ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new();
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

        internal async Task<bool> CreateUser(UserData data)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(data),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync($"{API}signup/", content);
                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        internal async Task<List<UserData>> GetUsers()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStreamAsync();
                var users = JsonSerializer.Deserialize<List<UserData>>(content);
                return users ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new();
            }
        }
    }
}
