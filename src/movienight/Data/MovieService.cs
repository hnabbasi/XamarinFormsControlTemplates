using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using MovieNight.Models;
using Newtonsoft.Json;

namespace MovieNight.Data
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey = "67b8e2cf";

        public MovieService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://www.omdbapi.com/")
            };
        }

        public async Task<List<Movie>> GetMovies(string keyword = null)
        {
            var retVal = new List<Movie>();
            try
            {
                var response = _client.GetAsync($"?s={keyword ?? "harry potter"}&apikey={_apiKey}");

                if (response.IsFaulted || response.IsCanceled)
                {
                    return retVal;
                }

                var content = await response.Result.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieSearch>(content);

                return result.Response ? result?.Search : retVal;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return retVal;
            }
        }
    }
}
