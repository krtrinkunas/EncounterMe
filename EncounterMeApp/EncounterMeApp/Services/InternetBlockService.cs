using EncounterMeApp.Models;
using EncounterMeApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(InternetBlockService))]
namespace EncounterMeApp.Services
{
    public class InternetBlockService : IBlockService
    {
        private HttpClient _httpClient;

        string baseUrl = "http://10.0.2.2:20082";

        public InternetBlockService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        public async Task AddBlock(Block block)
        {
            var response = await _httpClient.PostAsync("api/Block",
               new StringContent(JsonConvert.SerializeObject(block), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteBlock(Block block)
        {
            var response = await _httpClient.DeleteAsync($"api/Block/{block.Id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<Block> GetBlock(int id)
        {
            var response = await _httpClient.GetAsync($"api/Block/{id}");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject<Block>(responseAsString);
        }

        public async Task<IEnumerable<Block>> GetBlocks()
        {

            var response = await (_httpClient.GetAsync("api/Block")).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Block>>(responseAsString);
        }

        public async Task UpdateBlock(Block block)
        {
            var response = await _httpClient.PutAsync($"api/Block/{block.Id}",
                new StringContent(JsonConvert.SerializeObject(block), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
