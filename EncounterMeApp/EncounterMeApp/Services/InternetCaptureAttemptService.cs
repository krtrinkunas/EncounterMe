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
    public class InternetCaptureAttemptService : ICaptureAttemptService
    {
        private HttpClient _httpClient;

        string baseUrl = "http://10.0.2.2:20082";

        public InternetCaptureAttemptService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        public async Task AddCaptureAttempt(CaptureAttempt captureAttempt)
        {
            var response = await _httpClient.PostAsync("api/CaptureAttempt",
               new StringContent(JsonConvert.SerializeObject(captureAttempt), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCaptureAttempt(CaptureAttempt captureAttempt)
        {
            var response = await _httpClient.DeleteAsync($"api/CaptureAttempt/{captureAttempt.CaptureAttemptId}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<CaptureAttempt> GetCaptureAttempt(int id)
        {
            var response = await _httpClient.GetAsync($"api/CaptureAttempt/{id}");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject<CaptureAttempt>(responseAsString);
        }

        public async Task<IEnumerable<CaptureAttempt>> GetCaptureAttempts()
        {

            var response = await (_httpClient.GetAsync("api/CaptureAttempt")).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<CaptureAttempt>>(responseAsString);
        }

        public async Task UpdateCaptureAttempt(CaptureAttempt captureAttempt)
        {
            var response = await _httpClient.PutAsync($"api/CaptureAttempt/{captureAttempt.CaptureAttemptId}",
                new StringContent(JsonConvert.SerializeObject(captureAttempt), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
