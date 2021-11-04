using EncounterMeApp.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EncounterMeApp.Services
{
    public class InternetPlayerService : IPlayerService
    {
        private HttpClient _httpClient;

        
        string baseUrl = "http://10.0.2.2:20082";
        //HttpClient client;

        public InternetPlayerService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }


        /*
        public InternetPlayerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        */


        public async Task AddPlayer(Player player)
        {
            var response = await _httpClient.PostAsync("api/Players",
                new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePlayer(Player player)
        {
            var response = await _httpClient.DeleteAsync($"Players/{player.Id}"); //?

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdatePlayer(Player player)
        {
            var response = await _httpClient.PutAsync($"Players/{player.Id}", //?
                new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task<Player> GetPlayer(int id)
        {
            var response = await _httpClient.GetAsync($"api/Players/{id}"); //?

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject<Player>(responseAsString);
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            var response = await _httpClient.GetAsync("api/Players"); //?

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Player>>(responseAsString);
        }



        /*
        public async Task<IEnumerable<Player>> GetPlayers()
        {
            try
            {
                var httpResponse = await client.GetAsync("api/Player");
                var jsonStr = await httpResponse.Content.ReadAsStringAsync();

                var players = JsonConvert.DeserializeObject<IEnumerable<Player>>(jsonStr);
                return players;
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        Random random = new Random();
        public async Task AddPlayer(string nickName, int points)
        {
            var image = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png";
            var player = new Player
            {
                NickName = nickName,
                Points = points,
                ProfilePic = image,
                Id = random.Next(0, 10000),
                Email = "random@mail.com"
            };

            var json = JsonConvert.SerializeObject(player);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("api/Player", content);
            }
            catch(Exception ex)
            {

            }
            
        }

        public async Task RemovePlayer(int id)
        {
            try
            {
                var response = await client.DeleteAsync($"api/Player/{id}");
            }
            catch (Exception ex)
            {

            }
        }
        */
    }
}
