using BookmarkAndBlockbuster.Models;
using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Interfaces;
using BookmarkAndBlockbuster.Models.ViewModels;

namespace BookmarkAndBlockbuster.Controllers
{
    public class MoviePageController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IScreeningService _screeningService;
        private readonly IAuthorService _authorService;

        public MoviePageController(IMovieService MovieService, IScreeningService ScreeningService, IAuthorService AuthorService)
        {
            _movieService = MovieService;
            _screeningService = ScreeningService;
            _authorService = AuthorService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: /MoviePage/List
        public async Task<IActionResult> List()
        {
            IEnumerable<MovieDto?> MovieDtos = await _movieService.GetMovies();
            return View(MovieDtos);
        }

        // GET: /MoviePage/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            MovieDto? MovieDto = await _movieService.FindMovie(id);

            IEnumerable<ScreeningDto> AssociatedScreenings = await _screeningService.ListScreeningsForMovie(id);

            MovieDetails MovieInfo = new MovieDetails
            {
                Movie = MovieDto,
                Screenings = AssociatedScreenings
            };

            return View(MovieInfo);
        }

        // GET: /MoviePage/New
        [HttpGet]
        public async Task<IActionResult> New()
        {
            IEnumerable<Author> authors = await _authorService.GetAuthors();
            return View(authors);
        }

        // POST: /MoviePage/Add
        [HttpPost]
        public async Task<IActionResult> Add(Movie movie)
        {
            await _movieService.AddMovie(movie);

            return RedirectToAction("List", "MoviePage");
        }

        // GET : /MoviePage/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            MovieDto movie = await _movieService.FindMovie(id);
            IEnumerable<Author> authors = await _authorService.GetAuthors();

            MovieDetails movieInfo = new MovieDetails
            {
                Movie = movie,
                Authors = authors
            };

            return View(movieInfo);
        }

        // POST: /MoviePage/Update/{id}
        [HttpPost]
        public async Task<IActionResult> Update(int id, Movie movie)
        {
            await _movieService.PutMovie(id, movie);

            return RedirectToAction("Details", "MoviePage", new { id = id });
        }

        // GET: /MoviePage/ConfirmDelete/{id}
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            MovieDto movie = await _movieService.FindMovie(id);
            return View(movie);
        }

        // POST: /MoviePage/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _movieService.DeleteMovie(id);
            return RedirectToAction("List", "MoviePage");
        }

    }
}
