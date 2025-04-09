using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using BookmarkAndBlockbuster.Data.Migrations;
using BookmarkAndBlockbuster.Services;

namespace BookmarkAndBlockbuster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET - api/book/list
        [HttpGet(template: "List")]
        public async Task<ActionResult<IEnumerable<Book>>> ListBooks()
        {
            IEnumerable<Book> Books = await _bookService.GetBooks();
            return Ok(Books);

        }

        // GET - api/book/find/{id}
        [HttpGet("Find/{id}")]
        public async Task<ActionResult<Book>> FindBook(int id)
        {
            var book = await _bookService.FindBook(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST - api/book/add
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(Book book)
        {
            await _bookService.AddBook(book);
            return NoContent();
        }

        // PUT - api/book/update/{id}
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            var result = await _bookService.UpdateBook(id, book);
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

        // DELETE - api/book/delete/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBook(id);

            if (result == "Not Found") return NotFound();

            return NoContent();
        }

        [HttpGet(template: "ListBooksForAuthor/{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> ListBooksForAuthor(int id)
        {
            IEnumerable<Book> Books = await _bookService.ListBooksForAuthor(id);
            return Ok(Books);

        }

        [HttpGet(template: "ListBooksForMember/{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> ListBooksForMember(int id)
        {

            IEnumerable<Book> Books = await _bookService.ListBooksForMember(id);

            return Ok(Books);
        }
    }
}
