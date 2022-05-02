using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client
{
    public class MovieClient
    {
        private readonly string _movieUrl = "api/movies";
        private readonly string _jsonMediaType = "application/json";
        private HttpClient _client;

        public MovieClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:57863");
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Clear();
        }

        public HttpClient Client { get; }

        public async Task<IEnumerable<Movie>> GetMovies(CancellationToken token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _movieUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_jsonMediaType));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("qzip"));

            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, token))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();

                return stream.ReadAndDeserializeFromJson<List<Movie>>();
            }
        }
    }
}
