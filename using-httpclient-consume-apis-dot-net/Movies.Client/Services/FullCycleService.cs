using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    class FullCycleService : IIntegrationService
    {
        private readonly MovieClient _movieClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private CancellationTokenSource _ctx = new CancellationTokenSource(30000);
        private readonly string _jsonMediaType = "application/json";

        public FullCycleService(MovieClient movieClient, IHttpClientFactory httpClientFactory)
        {
            _movieClient = movieClient;
            _httpClientFactory = httpClientFactory;
        }

        public async Task Run()
        {
            await FullCycleRun(_ctx.Token);
        }

        public async Task FullCycleRun(CancellationToken cancellationToken)
        {
            var movies = await _movieClient.GetMovies(cancellationToken);
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

            try
            {
                using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            Console.WriteLine("The request movie cannot be found.");
                            return;
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
                        {
                            var validationErrors = stream.ReadAndDeserializeFromJson();
                            Console.WriteLine(validationErrors);
                            return;
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            return;
                        }

                        // will throw out with Exception
                        response.EnsureSuccessStatusCode();
                    }

                    var movies = stream.ReadAndDeserializeFromJson<List<Movie>>();
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"An operation was cancelled with message: {ex.Message}");
            }
        }
    }
}
