using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Interfaces;
using BookmarkAndBlockbuster.Services;

namespace BookmarkAndBlockbuster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService MovieService)
        {
            _movieService = MovieService;
        }

        [HttpGet(template:"GetMovies")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            IEnumerable<MovieDto> Movies = await _movieService.GetMovies();
            return Ok(Movies);
        }

        [HttpGet(template: "FindMovie/{id}")]

        public async Task<ActionResult<MovieDto>> FindMovie(int id)
        {
            MovieDto MovieDto = await _movieService.FindMovie(id);

            return Ok(MovieDto);
        }

        [HttpPost(template: "AddMovie")]

        public async Task<ActionResult<string>> AddMovie(Movie movie)
        {
            await _movieService.AddMovie(movie);

            return CreatedAtAction("FindMovie", new { id = movie.MovieId }, movie);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            var result = await _movieService.PutMovie(id, movie);

            if (result == "Bad Request")
            {
                return BadRequest();
            }

            if (result == "Not Found")
            {
                return NotFound();
            }

            if (result == "No Content")
            {
                return NoContent();
            }

            return NoContent();

        }

        [HttpDelete(template: "DeleteMovie/{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var result = await _movieService.DeleteMovie(id);

            if (result == "Not Found")
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet(template: "ListMoviesForAuthor/{id}")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> ListMoviesForAuthor(int id)
        {
            IEnumerable<MovieDto> Movies = await _movieService.ListMoviesForAuthor(id);
            return Ok(Movies);
        }

        [HttpGet(template: "ListMoviesForMember/{id}")]
        public async Task<ActionResult<IEnumerable<Movie>>> ListMoviesForMember(int id)
        {

            IEnumerable<Movie> Movies = await _movieService.ListMoviesForMember(id);

            return Ok(Movies);
        }
    }
}
