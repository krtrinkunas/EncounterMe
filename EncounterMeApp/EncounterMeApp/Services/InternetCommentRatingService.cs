using EncounterMeApp.Models;
using EncounterMeApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(InternetCommentRatingService))]
namespace EncounterMeApp.Services
{
    public class InternetCommentRatingService : ICommentRatingService
    {
        private HttpClient _httpClient;

        string baseUrl = "http://10.0.2.2:20082";

        public InternetCommentRatingService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        public async Task AddCommentRating(CommentRating commentRating)
        {
            var response = await _httpClient.PostAsync("api/CommentRating",
               new StringContent(JsonConvert.SerializeObject(commentRating), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCommentRating(CommentRating commentRating)
        {
            var response = await _httpClient.DeleteAsync($"api/CommentRating/{commentRating.CommentRatingId}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<CommentRating> GetCommentRating(int id)
        {
            var response = await _httpClient.GetAsync($"api/CommentRating/{id}");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject<CommentRating>(responseAsString);
        }

        public async Task<IEnumerable<CommentRating>> GetCommentRatings()
        {

            var response = await (_httpClient.GetAsync("api/CommentRating")).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<CommentRating>>(responseAsString);
        }

        public async Task UpdateCommentRating(CommentRating commentRating)
        {
            var response = await _httpClient.PutAsync($"api/CommentRating/{commentRating.CommentRatingId}",
                new StringContent(JsonConvert.SerializeObject(commentRating), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
