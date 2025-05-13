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
    }

    public class User(string username)
    {
        private readonly string API = IApi.BASE_URL + "user/";

        protected string UserName
        {
            set => username = value;
            get => username;
        }
        private static readonly HttpClient _httpClient = new(
            new SocketsHttpHandler() { PooledConnectionLifetime = TimeSpan.FromMinutes(5) }
        );

        public async Task<int> GetUserId()
        {
            var response = await _httpClient.GetAsync($"{API}");
            response.EnsureSuccessStatusCode();
            try
            {
                var responseContent = await response.Content.ReadAsStreamAsync();
                var users = JsonSerializer.Deserialize<List<UserData>>(responseContent);
                var user = users?.FirstOrDefault(u => u.Username == UserName);
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
    }
}
