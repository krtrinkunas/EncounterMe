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
        static string baseUrl = "http://10.0.2.2:5000";

        static HttpClient client;

        public InternetPlayerService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task<IEnumerable<Player>> GetPlayer()
        {
            var json = await client.GetStringAsync("api/Player");
            var players = JsonConvert.DeserializeObject<IEnumerable<Player>>(json);
            return players;
        }
    }
}
