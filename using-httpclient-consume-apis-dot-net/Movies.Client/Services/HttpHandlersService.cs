using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    public class HttpHandlersService : IIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CancellationTokenSource _ctx = new CancellationTokenSource();

        public HttpHandlersService(IHttpClientFactory httpClientFactory, MovieClient movieClient)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Run()
        {
            await GetMovieWithRepryPolicy(_ctx.Token);
        }

        private async Task GetMovieWithRepryPolicy(CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("MovieClient");

            var request = new HttpRequestMessage(HttpMethod.Get, "api/movies/5b1c2b4d-48c7-402a-80c3-cc796ad49ccc");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            using(var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("The request movie cannot be found.");
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
