using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Interfaces;

namespace BookmarkAndBlockbuster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/author/list
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<Author>>> ListAuthors()
        {
            IEnumerable<Author> Authors = await _authorService.GetAuthors();
            return Ok(Authors);
        }

        // GET: api/author/find/{id}
        [HttpGet(template: "Find/{id}")]
        public async Task<ActionResult<Author>> FindAuthor(int id)
        {
            var result = await _authorService.FindAuthor(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/author/add
        [HttpPost(template: "AddAuthor")]
        public async Task<IActionResult> AddAuthor(Author author)
        {
            await _authorService.AddAuthor(author);
            return NoContent();
        }

        // PUT: api/author/update/{id}
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            var result = await _authorService.UpdateAuthor(id, author);

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

        // DELETE: api/author/delete/{id}
        [HttpDelete(template: "Delete/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthor(id);

            if (result == "Not Found")
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

