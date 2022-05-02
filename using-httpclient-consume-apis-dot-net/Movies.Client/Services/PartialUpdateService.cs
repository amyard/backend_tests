using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.Services
{
    public class PartialUpdateService : IIntegrationService
    {
        private static HttpClient _httpClient = new HttpClient();
        private readonly string _movieUrl = "api/movies";
        private readonly string _jsonMediaType = "application/json";
        private readonly string _jsonPatchMediaType = "application/json-patch+json";

        public PartialUpdateService()
        {
            _httpClient.BaseAddress = new Uri("http://localhost:57863");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
        }
        
        public async Task Run()
        {
            await PatchResource();
            //await PatchResourceShortCut();
        } 
        
        public async Task PatchResource()
        {
            //var patсhDoc = new JsonPatchDocument();
            var patсhDoc = new JsonPatchDocument<MovieForUpdate>();
            patсhDoc.Replace(m => m.Title, "Updated title");
            patсhDoc.Remove(m => m.Description);

            var serializedChangeSr = JsonConvert.SerializeObject(patсhDoc);
            var request = new HttpRequestMessage(HttpMethod.Patch, _movieUrl + "/" + "5b1c2b4d-48c7-402a-80c3-cc796ad49c6b");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_jsonMediaType));
            request.Content = new StringContent(serializedChangeSr);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(_jsonPatchMediaType);
            
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var updatedMovie = JsonConvert.DeserializeObject<Movie>(content);
        } 

        public async Task PatchResourceShortCut()
        {
            var patсhDoc = new JsonPatchDocument<MovieForUpdate>();
            patсhDoc.Replace(m => m.Title, "Updated title");
            patсhDoc.Remove(m => m.Description);

            var response = await _httpClient.PatchAsync(_movieUrl + "/" + "5b1c2b4d-48c7-402a-80c3-cc796ad49c6b",
                new StringContent(JsonConvert.SerializeObject(patсhDoc), Encoding.UTF8, _jsonPatchMediaType));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var updatedMovie = JsonConvert.DeserializeObject<Movie>(content);
        }
    }
}
