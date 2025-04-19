using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Interfaces;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Models.ViewModels; 

namespace BookmarkAndBlockbuster.Controllers
{
    public class BookPageController : Controller
    {

        private readonly IBookService _bookService;
        private readonly IBooksLogService _booksLogService;
        private readonly IAuthorService _authorService;

        public BookPageController(IBookService BookService, IBooksLogService BooksLogService, IAuthorService AuthorService)
        {
            _bookService = BookService;
            _booksLogService = BooksLogService;
            _authorService = AuthorService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET -> /BookPage/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<Book> Books = await _bookService.GetBooks();
            return View(Books);
        }

        // GET -> /BookPage/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Book Book = await _bookService.FindBook(id);
            IEnumerable<BooksLogDto> BookLogs = await _booksLogService.GetBooksLogForBook(id);

            BookDetails BooksInfo = new BookDetails()
            {
                Book = Book,
                BooksLog = BookLogs
            };

            return View(BooksInfo);
        }

        // GET -> /BookPage/New
        [HttpGet]
        public async Task<IActionResult> New()
        {
            IEnumerable<Author> authors = await _authorService.GetAuthors();
            return View(authors);
        }

        // POST -> /BookPage/Add
        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            await _bookService.AddBook(book);
            return RedirectToAction("List", "BookPage");
        }

        // GET -> /BookPage/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Book book = await _bookService.FindBook(id);
            IEnumerable<Author> authors = await _authorService.GetAuthors();

             EditBookViewModel booksInfo = new EditBookViewModel()
            {
                Book = book,
                Authors = authors
            };

            return View(booksInfo);
        }

        // POST -> /BookPage/Update/{id}
        [HttpPost]
        public async Task<IActionResult> Update(int id, Book book)
        {
            await _bookService.UpdateBook(id, book);

            return RedirectToAction("Details", "BookPage", new { id = id });
        }

        // Get -> BookPage/ConfirmDelete/{id}
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            Book book = await _bookService.FindBook(id);
            return View(book);
        }

        // Post ->BookPage/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBook(id);
            return RedirectToAction("List", "BookPage");
        }
    }
}
