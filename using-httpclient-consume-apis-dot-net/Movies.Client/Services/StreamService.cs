using Movies.Client.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    public class StreamService : IIntegrationService
    {
        private static HttpClient _httpClient = new HttpClient();
        private readonly string _movieUrl = "api/movies";
        private readonly string _jsonMediaType = "application/json";

        public StreamService()
        {
            _httpClient.BaseAddress = new Uri("http://localhost:57863");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task Run()
        {
            //await GetPosterWithStream();
            await TestGetPosterWithoutStream();
            await TestGetPosterWithStream();
            await TestGetPosterWithStreamAndCompletionMode();
        }

        public async Task GetPosterWithoutStream()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_movieUrl}/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_jsonMediaType));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var poster = JsonConvert.DeserializeObject<Poster>(content);
        }

        public async Task GetPosterWithStream()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_movieUrl}/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_jsonMediaType));

            var response = await _httpClient.SendAsync(request);

            using (var stream = await response.Content.ReadAsStreamAsync())
            { 
                response.EnsureSuccessStatusCode();
                var poster = stream.ReadAndDeserializeFromJson<Poster>();
            }
        }

        public async Task GetPosterWithStreamAndCompletionMode()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_movieUrl}/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_jsonMediaType));

            // start to work not when all data is received
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                response.EnsureSuccessStatusCode();
                var poster = stream.ReadAndDeserializeFromJson<Poster>();
            }
        }

        #region tests
        public async Task TestGetPosterWithoutStream()
        {
            // warmup
            await GetPosterWithoutStream();

            var stopWatch = Stopwatch.StartNew();

            for (int i = 0; i < 200; i++)
            {
                await GetPosterWithoutStream();
            }

            stopWatch.Stop();
            Console.WriteLine($"WithoutStream: {stopWatch.ElapsedMilliseconds}. averaging - {stopWatch.ElapsedMilliseconds / 200} milliseconds/request.");
        }

        public async Task TestGetPosterWithStream()
        {
            // warmup
            await GetPosterWithStream();

            var stopWatch = Stopwatch.StartNew();

            for (int i = 0; i < 200; i++)
            {
                await GetPosterWithStream();
            }

            stopWatch.Stop();
            Console.WriteLine($"GetPosterWithStream: {stopWatch.ElapsedMilliseconds}. averaging - {stopWatch.ElapsedMilliseconds / 200} milliseconds/request.");
        }

        public async Task TestGetPosterWithStreamAndCompletionMode()
        {
            // warmup
            await GetPosterWithStreamAndCompletionMode();

            var stopWatch = Stopwatch.StartNew();

            for (int i = 0; i < 200; i++)
            {
                await GetPosterWithStreamAndCompletionMode();
            }

            stopWatch.Stop();
            Console.WriteLine($"GetPosterWithStreamAndCompletionMode: {stopWatch.ElapsedMilliseconds}. averaging - {stopWatch.ElapsedMilliseconds / 200} milliseconds/request.");
        }
        #endregion
    }
}
