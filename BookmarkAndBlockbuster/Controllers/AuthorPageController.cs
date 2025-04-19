using BookmarkAndBlockbuster.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Models.ViewModels;
using BookmarkAndBlockbuster.Services;

namespace BookmarkAndBlockbuster.Controllers
{
    public class AuthorPageController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        private readonly IMovieService _movieService;
        public AuthorPageController(IAuthorService AuthorService, IBookService BookService, IMovieService movieService)
        {
            _authorService = AuthorService;
            _bookService = BookService;
            _movieService = movieService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET -> /AuthorPage/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<Author> authors = await _authorService.GetAuthors();
            return View(authors);
        }

        // GET -> /AuthorPage/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Author author = await _authorService.FindAuthor(id);
            IEnumerable<Book> books = await _bookService.ListBooksForAuthor(id);
            IEnumerable<MovieDto> movies = await _movieService.ListMoviesForAuthor(id);

            AuthorDetails AuthorInfo = new AuthorDetails()
            {
                Author = author,
                AuthorBooks = books,
                AuthorMovies = movies
            };
            return View(AuthorInfo);
        }

        // GET -> /AuthorPage/New
        public IActionResult New()
        {
            return View();
        }

        // POST -> AuthorPage/Add
        [HttpPost]
        public async Task<IActionResult> Add(Author Author)
        {
            await _authorService.AddAuthor(Author);

            return RedirectToAction("List", "AuthorPage");
        }

        // GET -> AuthorPage/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Author author = await _authorService.FindAuthor(id);

            return View(author);
        }

        // POST -> AuthorPage/Update/{id}
        [HttpPost]
        public async Task<IActionResult> Update(int id, Author Author)
        {
            await _authorService.UpdateAuthor(id, Author);
            return RedirectToAction("Details", "AuthorPage", new { id = id });
        }

        // GET -> /AuthorPage/ConfirmDelete/{id}
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            Author author = await _authorService.FindAuthor(id);

            return View(author);
        }

        // POST -> /AuthorPage/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAuthor(id);

            return RedirectToAction("List", "AuthorPage");
        }
    }
}
