using Microsoft.EntityFrameworkCore;
using STUDY.MVC.Movies.Data;
using STUDY.MVC.Movies.Models;

namespace STUDY.MVC.Movies.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesContext moviesContext;

        public MovieRepository(MoviesContext moviesContext)
        {
            this.moviesContext = moviesContext;
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await moviesContext.Movie.ToListAsync();
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            await moviesContext.Movie.AddAsync(movie);
            await moviesContext.SaveChangesAsync();
            return movie;
        }

        public Task<Movie?> DeleteAsync(int tag)
        {
            throw new NotImplementedException();
        }

        public Task<Movie?> GetAsync(int? id)
        {
            return moviesContext.Movie.FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<Movie?> UpdateAsync(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
