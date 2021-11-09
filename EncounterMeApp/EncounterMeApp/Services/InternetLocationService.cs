using EncounterMeApp.Models;
using EncounterMeApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(InternetLocationService))]
namespace EncounterMeApp.Services
{
    public class InternetLocationService : ILocationService
    {
        private HttpClient _httpClient;

        string baseUrl = "http://10.0.2.2:20082";

        public InternetLocationService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        public async Task AddLocation(MyLocation location)
        {
            var response = await _httpClient.PostAsync("api/Location",
               new StringContent(JsonConvert.SerializeObject(location), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteLocation(MyLocation location)
        {
            var response = await _httpClient.DeleteAsync($"api/Location/{location.Id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<MyLocation> GetLocation(int id)
        {
            var response = await _httpClient.GetAsync($"api/Location/{id}");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject<MyLocation>(responseAsString);
        }

        public async Task<IEnumerable<MyLocation>> GetLocations()
        {
            var response = await _httpClient.GetAsync("api/Location");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<MyLocation>>(responseAsString);
        }

        public async Task UpdateLocation(MyLocation location)
        {
            var response = await _httpClient.PutAsync($"api/Location/{location.Id}", //?
                new StringContent(JsonConvert.SerializeObject(location), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
