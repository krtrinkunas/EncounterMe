using EncounterMeApp.Models;
using EncounterMeApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(InternetFriendService))]
namespace EncounterMeApp.Services
{
    public class InternetFriendService : IFriendService
    {
        private HttpClient _httpClient;

        string baseUrl = "http://10.0.2.2:20082";

        public InternetFriendService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        public async Task AddFriend(Friend friend)
        {
            var response = await _httpClient.PostAsync("api/Friend",
               new StringContent(JsonConvert.SerializeObject(friend), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteFriend(Friend friend)
        {
            var response = await _httpClient.DeleteAsync($"api/Friend/{friend.Id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<Friend> GetFriend(int id)
        {
            var response = await _httpClient.GetAsync($"api/Friend/{id}");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject<Friend>(responseAsString);
        }

        public async Task<IEnumerable<Friend>> GetFriends()
        {

            var response = await (_httpClient.GetAsync("api/Friend")).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Friend>>(responseAsString);
        }

        public async Task UpdateFriend(Friend friend)
        {
            var response = await _httpClient.PutAsync($"api/Friend/{friend.Id}",
                new StringContent(JsonConvert.SerializeObject(friend), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
