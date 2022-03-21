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
    public class InternetCommentService : ICommentService
    {
        private HttpClient _httpClient;

        string baseUrl = "http://10.0.2.2:20082";

        public InternetCommentService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        public async Task AddComment(Comment comment)
        {
            var response = await _httpClient.PostAsync("api/Comment",
               new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteComment(Comment comment)
        {
            var response = await _httpClient.DeleteAsync($"api/Comment/{comment.CommentId}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<Comment> GetComment(int id)
        {
            var response = await _httpClient.GetAsync($"api/Comment/{id}");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject<Comment>(responseAsString);
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {

            var response = await (_httpClient.GetAsync("api/Comment")).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Comment>>(responseAsString);
        }

        public async Task UpdateComment(Comment comment)
        {
            var response = await _httpClient.PutAsync($"api/Comment/{comment.CommentId}",
                new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
