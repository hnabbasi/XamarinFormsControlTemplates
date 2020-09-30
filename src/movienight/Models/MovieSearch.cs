using System.Collections.Generic;

namespace MovieNight.Models
{
    public class MovieSearch
    {
        public List<Movie> Search { get; set; }
        public int TotalResults { get; set; }
        public bool Response { get; set; }
        public string Error { get; set; }
    }
}
