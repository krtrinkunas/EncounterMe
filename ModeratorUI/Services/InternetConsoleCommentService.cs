using EncounterMeApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EncounterMeApp.Services
{
    public class InternetConsoleCommentService : IConsoleCommentService
    {
        private HttpClient _httpClient;

        string baseUrl = "http://localhost:20082";

        public InternetConsoleCommentService()
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
            Console.WriteLine("Getting comment...");
            var response = await _httpClient.GetAsync($"api/Comment/{id}");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();

            var com = JsonConvert.DeserializeObject<Comment>(responseAsString);

            return com;
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
