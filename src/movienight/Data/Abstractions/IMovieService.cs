using System.Collections.Generic;
using System.Threading.Tasks;
using MovieNight.Models;

namespace MovieNight.Data
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMovies(string keyword = null);
    }
}
