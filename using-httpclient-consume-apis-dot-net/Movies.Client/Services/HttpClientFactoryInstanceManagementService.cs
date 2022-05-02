using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    public class HttpClientFactoryInstanceManagementService : IIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _movieUrl = "http://localhost:57863/api/movies";
        private readonly string _jsonMediaType = "application/json";
        private CancellationTokenSource _ctx = new CancellationTokenSource();

        public HttpClientFactoryInstanceManagementService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Run()
        {
            // await TestDisposeHttpClient(_ctx.Token);
            // await TestReuseHttpClient(_ctx.Token);
            //await GetMovieWithHttpClientDactory(_ctx.Token);
            await GetMovieWithNamedHttpClientDactory(_ctx.Token);
        }

        public async Task GetMovieWithNamedHttpClientDactory(CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("MovieClient");
            var request = new HttpRequestMessage(HttpMethod.Get, "api/movies");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_jsonMediaType));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("qzip"));

            using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();

                var movies = stream.ReadAndDeserializeFromJson<List<Movie>>();
            }
        }

        public async Task GetMovieWithHttpClientDactory(CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _movieUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_jsonMediaType));

            using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();

                var movies = stream.ReadAndDeserializeFromJson<List<Movie>>();
            }
        }

        public async Task TestDisposeHttpClient(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 10; i++)
            {
                using(var httpClient = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://google.com");

                    using(var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                    {
                        var stream = await response.Content.ReadAsStreamAsync();
                        response.EnsureSuccessStatusCode();

                        Console.WriteLine($"Response completed with status code - {response.StatusCode}.");
                    }
                }
            }
        }
        public async Task TestReuseHttpClient(CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();

            for (int i = 0; i < 10; i++)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://google.com");

                using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine($"Response completed with status code - {response.StatusCode}.");
                }
            }
        }
    }
}
