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
    public class InternetPlayerService
    {

        string baseUrl = "http://10.0.2.2:20082";
        HttpClient client;

        public InternetPlayerService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }


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
            /*
            if(!response.IsSuccessStatusCode)
            {

            }
            */
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
            
            /*
            if(!response.IsSuccessStatusCode)
            {

            }
            */
        }

    }
}
