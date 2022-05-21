using EncounterMeApp.Models;
using EncounterMeApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(InternetFriendRequestService))]
namespace EncounterMeApp.Services
{
    public class InternetFriendRequestService : IFriendRequestService
    {
        private HttpClient _httpClient;

        string baseUrl = "http://10.0.2.2:20082";

        public InternetFriendRequestService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        public async Task AddFriendRequest(FriendRequest friendRequest)
        {
            var response = await _httpClient.PostAsync("api/FriendRequest",
               new StringContent(JsonConvert.SerializeObject(friendRequest), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteFriendRequest(FriendRequest friendRequest)
        {
            var response = await _httpClient.DeleteAsync($"api/FriendRequest/{friendRequest.Id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<FriendRequest> GetFriendRequest(int id)
        {
            var response = await _httpClient.GetAsync($"api/FriendRequest/{id}");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject<FriendRequest>(responseAsString);
        }

        public async Task<IEnumerable<FriendRequest>> GetFriendRequests()
        {

            var response = await (_httpClient.GetAsync("api/FriendRequest")).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<FriendRequest>>(responseAsString);
        }

        public async Task<IEnumerable<FriendRequest>> GetFriendRequests(int id)
        {
            var response = await _httpClient.GetAsync($"api/FriendRequest/{id}");
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<FriendRequest>>(responseAsString);
        }

        public async Task UpdateFriendRequest(FriendRequest friendRequest)
        {
            var response = await _httpClient.PutAsync($"api/FriendRequest/{friendRequest.Id}",
                new StringContent(JsonConvert.SerializeObject(friendRequest), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
