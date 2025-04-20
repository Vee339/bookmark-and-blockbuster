using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Models.ViewModels;
using BookmarkAndBlockbuster.Interfaces;
using BookmarkAndBlockbuster.Services;

namespace BookmarkAndBlockbuster.Controllers
{
    public class HallPageController : Controller
    {
        private readonly IHallService _hallService;
        private readonly IScreeningService _screeningService;

        public HallPageController(IHallService HallService, IScreeningService ScreeningService)
        {
            _hallService = HallService;
            _screeningService = ScreeningService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: /HallPage/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<Hall> Halls = await _hallService.GetHalls();

            return View(Halls);
        }

        // GET: /HallPage/Details/{id}

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Hall hall = await _hallService.FindHall(id);
            IEnumerable<ScreeningDto> screenings = await _screeningService.ListScreeningsForHall(id);

            HallDetails hallInfo = new HallDetails()
            {
                Hall = hall,
                Screenings = screenings
            };

            return View(hallInfo);
        }

        // GET: /HallPage/New
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        // POST: /HallPage/Add
        [HttpPost]
        public async Task<IActionResult> Add(Hall hall)
        {
            await _hallService.AddHall(hall);
            return RedirectToAction("List", "HallPage");
        }

        // GET: /HallPage/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Hall hall = await _hallService.FindHall(id);
            return View(hall);
        }

        // POST: /HallPage/Update/{id}
        [HttpPost]
        public async Task<IActionResult> Update(int id, Hall hall)
        {
            await _hallService.EditHall(id, hall);
            return RedirectToAction("Details", "HallPage", new { id = id });
        }

        // GET: /HallPage/ConfirmDelete/{id}
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            Hall hall = await _hallService.FindHall(id);
            return View(hall);
        }

        // POST: /HallPage/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _hallService.DeleteHall(id);
            return RedirectToAction("List", "HallPage");
        }
    }
}
