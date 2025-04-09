using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Interfaces;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Data;
using Microsoft.EntityFrameworkCore;

namespace BookmarkAndBlockbuster.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieDto>> GetMovies()
        {
            List<Movie> Movies = await _context.Movies.Include(m => m.Author).ToListAsync();

            List<MovieDto> MovieDtos = new List<MovieDto>();

            foreach (Movie Movie in Movies)
            {
                MovieDto MovieDto = new MovieDto
                {
                    MovieId = Movie.MovieId,
                    MovieName = Movie.Title,
                    Genre = Movie.Genre,
                    ReleaseYear = Movie.ReleaseYear,
                    AuthorName = Movie.Author.AuthorName
                };


                MovieDtos.Add(MovieDto);
            }
            return MovieDtos;
        }

        public async Task<MovieDto> FindMovie(int id)
        {
            Movie Movie = await _context.Movies.Include(m => m.Author).Where(m => m.MovieId == id).FirstOrDefaultAsync();

            MovieDto MovieDto = new MovieDto
            {
                MovieId = Movie.MovieId,
                MovieName = Movie.Title,
                Genre = Movie.Genre,
                ReleaseYear = Movie.ReleaseYear,
                AuthorName = Movie.Author.AuthorName
            };

            return MovieDto;
        }

        public async Task AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);

            await _context.SaveChangesAsync();
        }

        public async Task<string> PutMovie(int id, Movie movie)
        {
            if (id != movie.MovieId)
            {
                return "Bad Request";
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return "Not Found";
                }
                else
                {
                    throw;
                }
            }


            return "No Content";
        }

        public async Task<string> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return "Not Found";
            }

            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();

            return "No Content";
        }

        public async Task<IEnumerable<MovieDto>> ListMoviesForAuthor(int id)
        {
            List<Movie> Movies = await _context.Movies.Include(m => m.Author).Where(m => m.AuthorId == id).ToListAsync();

            List<MovieDto> MovieDtos = new List<MovieDto>();

            foreach (Movie Movie in Movies)
            {
                MovieDto MovieDto = new MovieDto
                {
                    MovieId = Movie.MovieId,
                    MovieName = Movie.Title,
                    Genre = Movie.Genre,
                    ReleaseYear = Movie.ReleaseYear,
                    AuthorName = Movie.Author.AuthorName
                };


                MovieDtos.Add(MovieDto);
            }
            return MovieDtos;
        }

        public async Task<IEnumerable<Movie>> ListMoviesForMember(int id)
        {

            List<Movie> Movies = await _context.Movies.Join(_context.MoviesLogs, Movie => Movie.MovieId, MoviesLog => MoviesLog.MovieId, (Movie, MoviesLog) => new { Movie, MoviesLog}).Where(ms => ms.MoviesLog.MemberId == id).Select(ms => ms.Movie).ToListAsync();

            return Movies;
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }

}
