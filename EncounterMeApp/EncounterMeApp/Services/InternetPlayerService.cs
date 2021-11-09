using EncounterMeApp.Models;
using EncounterMeApp.Services;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly:Dependency(typeof(InternetPlayerService))]
namespace EncounterMeApp.Services
{
    public class InternetPlayerService : IPlayerService
    {
        private HttpClient _httpClient;

        
        string baseUrl = "http://10.0.2.2:20082";

        public InternetPlayerService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task AddPlayer(Player player)
        {
            var response = await _httpClient.PostAsync("api/Player",
                new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePlayer(Player player)
        {
            var response = await _httpClient.DeleteAsync($"api/Player/{player.Id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdatePlayer(Player player)
        {
            var response = await _httpClient.PutAsync($"api/Player/{player.Id}",
                new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task<Player> GetPlayer(int id)
        {
            var response = await _httpClient.GetAsync($"api/Player/{id}");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject<Player>(responseAsString);
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            var response = await _httpClient.GetAsync("api/Player");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Player>>(responseAsString);
        }
    }
}
