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
    public class InternetLocationRatingService : ILocationRatingService
    {
        private HttpClient _httpClient;

        string baseUrl = "http://10.0.2.2:20082";

        public InternetLocationRatingService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        public async Task AddLocationRating(LocationRating locationRating)
        {
            var response = await _httpClient.PostAsync("api/LocationRating",
               new StringContent(JsonConvert.SerializeObject(locationRating), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteLocationRating(LocationRating locationRating)
        {
            var response = await _httpClient.DeleteAsync($"api/LocationRating/{locationRating.LocationRatingId}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<LocationRating> GetLocationRating(int id)
        {
            var response = await _httpClient.GetAsync($"api/LocationRating/{id}");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject<LocationRating>(responseAsString);
        }

        public async Task<IEnumerable<LocationRating>> GetLocationRatings()
        {

            var response = await (_httpClient.GetAsync("api/LocationRating")).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<LocationRating>>(responseAsString);
        }

        public async Task UpdateLocationRating(LocationRating locationRating)
        {
            var response = await _httpClient.PutAsync($"api/LocationRating/{locationRating.LocationRatingId}",
                new StringContent(JsonConvert.SerializeObject(locationRating), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
