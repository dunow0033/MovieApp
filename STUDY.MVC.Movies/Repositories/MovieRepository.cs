using Microsoft.EntityFrameworkCore;
using STUDY.MVC.Movies.Data;
using STUDY.MVC.Movies.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

        public async Task<Movie?> UpdateAsync(Movie movie)
        {
            var existingMovie = await moviesContext.Movie.FindAsync(movie.Id);

            if (existingMovie != null) 
            {
                try
                {
                    existingMovie.Title = movie.Title;
                    existingMovie.ReleaseDate = movie.ReleaseDate;
                    existingMovie.Price = movie.Price;
                    existingMovie.Genre = movie.Genre;
                    existingMovie.Rating = movie.Rating;
                    await moviesContext.SaveChangesAsync();
                    return existingMovie;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return null;
        }

        public async Task<Movie?> DeleteAsync(int id)
        {
            //await moviesContext.Movie.Remove(id);
            throw new NotImplementedException();
        }

        public async Task<Movie?> GetAsync(int id)
        {
            return await moviesContext.Movie.FirstOrDefaultAsync(m => m.Id == id);
        }

        private bool MovieExists(int id)
        {
            return moviesContext.Movie.Any(e => e.Id == id);
        }

        Task<Movie?> IMovieRepository.GetAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
