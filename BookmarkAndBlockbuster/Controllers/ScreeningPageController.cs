using BookmarkAndBlockbuster.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Models.ViewModels;
using System.Runtime.CompilerServices;

namespace BookmarkAndBlockbuster.Controllers
{
    public class ScreeningPageController : Controller
    {
        private readonly IScreeningService _screeningService;
        private readonly IMovieService _movieService;
        private readonly IHallService _hallService;

        public ScreeningPageController(IScreeningService ScreeningService, IMovieService MovieService, IHallService HallService)
        {
            _screeningService = ScreeningService;
            _movieService = MovieService;
            _hallService = HallService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET -> /ScreeningPage/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<ScreeningDto> Screenings = await _screeningService.GetScreenings();
            return View(Screenings);
        }

        // GET -> /ScreeningPage/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ScreeningDto screening = await _screeningService.FindScreening(id);
            return View(screening);
        }

        // Get -> /ScreeningPage/New
        [HttpGet]
        public async Task<IActionResult> New()
        {
            IEnumerable<MovieDto> movies = await _movieService.GetMovies();
            IEnumerable<Hall> halls = await _hallService.GetHalls();

            MoviesHalls moviesHalls = new MoviesHalls()
            {
                Movies = movies,
                Halls = halls
            };

            return View(moviesHalls);
        }

        // POST -> /ScreeningPage/Add
        [HttpPost]
        public async Task<IActionResult> Add(Screening screening)
        {
            await _screeningService.AddScreening(screening);

            return RedirectToAction("List", "ScreeningPage");
        }

        // GET -> /ScreeningPage/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ScreeningDto screening = await _screeningService.FindScreening(id);
            IEnumerable<MovieDto> movies = await _movieService.GetMovies();
            IEnumerable<Hall> halls = await _hallService.GetHalls();

            MoviesHalls moviesHalls = new MoviesHalls
            {
                Screening = screening,
                Movies = movies,
                Halls = halls
            };

            return View(moviesHalls);
        }

        // POST -> /ScreeningPage/Update/{id}
        [HttpPost]
        public async Task<IActionResult> Update(int id, Screening screening)
        {
            await _screeningService.EditScreening(id, screening);
            return RedirectToAction("Details", "ScreeningPage", new { id = id });
        }

        // GET -> /ScreeningPage/ConfirmDelete/{id}
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            ScreeningDto screening = await _screeningService.FindScreening(id);
            return View(screening);
        }

        // POST -> /ScreeningPage/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _screeningService.DeleteScreening(id);
            return RedirectToAction("List", "ScreeningPage");
        }
    }
}
