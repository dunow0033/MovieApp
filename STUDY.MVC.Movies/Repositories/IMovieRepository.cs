using STUDY.MVC.Movies.Models;

namespace STUDY.MVC.Movies.Repositories
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllMoviesAsync();
        Task<Movie?> GetAsync(int? id);
        Task<Movie?> AddAsync(Movie movie);
        Task<Movie?> UpdateAsync(Movie movie);
        Task<Movie?> DeleteAsync(Movie movie);
    }
}
