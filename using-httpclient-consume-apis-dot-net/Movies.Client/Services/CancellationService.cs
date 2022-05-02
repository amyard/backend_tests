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
    public class CancellationService : IIntegrationService
    {
        private static HttpClient _httpClient = new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip
        });
        private readonly string _movieUrl = "api/movies";
        private readonly string _jsonMediaType = "application/json";
        private CancellationTokenSource _ctx = new CancellationTokenSource();

        public CancellationService()
        {
            _httpClient.BaseAddress = new Uri("http://localhost:57863");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task Run()
        {
            _ctx.CancelAfter(1000);
            await GetTrailerAndCancel(_ctx.Token);
        }

        private async Task GetTrailerAndCancel(CancellationToken token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_movieUrl}/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters/trailers/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_jsonMediaType));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            try
            {
                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, token))
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    response.EnsureSuccessStatusCode();
                    var trailer = stream.ReadAndDeserializeFromJson<Trailer>();
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"An operation was cancelled with message: {ex.Message}");
            }
        }
    }
}
