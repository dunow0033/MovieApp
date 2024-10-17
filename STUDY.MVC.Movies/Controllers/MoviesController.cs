using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using STUDY.MVC.Movies.Data;
using STUDY.MVC.Movies.Models;
using STUDY.MVC.Movies.Repositories;

namespace STUDY.MVC.Movies.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieRepository movieRepository;

    public MoviesController(MovieRepository movieRepository)
    {
        this.movieRepository = movieRepository;
    }

    // GET: Movies
    public async Task<IActionResult> Index(string movieGenre, string searchString)
    {
        var movies = await movieRepository.GetAllMoviesAsync();
        // Use LINQ to get list of genres.
        var genreQuery = movies.Select(m => m.Genre).Distinct().OrderBy(genre => genre);

        if (!string.IsNullOrEmpty(searchString))
        {
            movies = movies.Where(m => m.Title != null && m.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (!string.IsNullOrEmpty(movieGenre))
        {
            movies = movies.Where(x => x.Genre == movieGenre).ToList();
        }

        var movieGenreVM = new MovieGenreViewModel
        {
            Genres = new SelectList(genreQuery),
            Movies = movies
        };

        return View(movieGenreVM);
    }

    // GET: Movies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || movieRepository.GetAllMoviesAsync() == null)
        {
            return NotFound();
        }

        var movie = await movieRepository.GetAsync(id.Value);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: Movies/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Movies/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price, Rating")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            await movieRepository.AddAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: Movies/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        if (id == null || movieRepository.GetAllMoviesAsync() == null)
        {
            return NotFound();
        }

        var movie = await movieRepository.GetAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    // POST: Movies/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price, Rating")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var updatedMovie = await movieRepository.UpdateAsync(movie);
            if(updatedMovie == null) 
            {
                return NotFound();
            }
           
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: Movies/Delete/5
    //public async Task<IActionResult> Delete(int id)
    //{
    //    if (id == null || movieRepository.GetAllMoviesAsync() == null)
    //    {
    //        return NotFound();
    //    }

    //    var movie = await this.movieRepository.Movie
    //        .FirstOrDefaultAsync(m => m.Id == id);
    //    if (movie == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(movie);
    //}

    //// POST: Movies/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(int id)
    //{
    //    if (_context.Movie == null)
    //    {
    //        return Problem("Entity set 'MoviesContext.Movie'  is null.");
    //    }
    //    var movie = await _context.Movie.FindAsync(id);
    //    if (movie != null)
    //    {
    //        _context.Movie.Remove(movie);
    //    }
        
    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}
}
