using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    public class DealingWithErrorsAndFaultsService : IIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _movieUrl = "http://localhost:57863/api/movies";
        private readonly string _jsonMediaType = "application/json";
        private CancellationTokenSource _ctx = new CancellationTokenSource();

        public DealingWithErrorsAndFaultsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Run()
        {
            // await RunQuery1(_ctx.Token);
            await HandleValidationError(_ctx.Token);
        }

        public async Task RunQuery1(CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("MovieClient");
            var request = new HttpRequestMessage(HttpMethod.Get, "api/movies/d8663e5e-7494-4f81-8739-6e0de1bea711");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_jsonMediaType));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("qzip"));

            using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                if(!response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("The request movie cannot be found.");
                        return;
                    }
                    else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return;
                    }

                    // will throw out with Exception
                    response.EnsureSuccessStatusCode();
                }    

                var stream = await response.Content.ReadAsStreamAsync();
                var movies = stream.ReadAndDeserializeFromJson<List<Movie>>();
            }
        }

        public async Task HandleValidationError(CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("MovieClient");

            var movieForCreateInvalid = new MovieForUpdate()
            {
                Title = "Some title"
            };
            var serializedMovie = JsonSerializer.Serialize(movieForCreateInvalid);

            using (var request = new HttpRequestMessage(HttpMethod.Post, "api/movies"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_jsonMediaType));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                request.Content = new StringContent(serializedMovie);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(_jsonMediaType);

                using(var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
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

                    var movie = stream.ReadAndDeserializeFromJson<Movie>();
                }
            }
        }
    }
}
