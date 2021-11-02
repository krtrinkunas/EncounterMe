using EncounterMeApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EncounterMeApp.Services
{
    public class InternetPlayerService
    {
        static string baseUrl = "http://10.0.2.2:5000"; //???
        static HttpClient client;

        public InternetPlayerService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public static async Task<IEnumerable<Player>> GetPlayers()
        {
            var json = await client.GetStringAsync("api/Player");
            var players = JsonConvert.DeserializeObject<IEnumerable<Player>>(json);
            return players;
        }

        static Random random = new Random();
        public static async Task AddPlayer(string nickName, int points)
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

            var response = await client.PostAsync("api/Player", content);

            if(!response.IsSuccessStatusCode)
            {

            }
        }

        public static async Task RemovePlayer(int id)
        {
            var response = await client.DeleteAsync($"api/Player/{id}");
            if(!response.IsSuccessStatusCode)
            {

            }
        }

    }
}
