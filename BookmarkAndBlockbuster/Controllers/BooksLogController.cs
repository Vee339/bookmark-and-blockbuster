using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Data;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Interfaces;
using BookmarkAndBlockbuster.Services;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Linq;
using BookmarkAndBlockbuster.Data.Migrations;

namespace BookmarkAndBlockbuster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksLogController : ControllerBase
    {
        private readonly IBooksLogService _booksLogService;

        public BooksLogController(IBooksLogService BooksLogService)
        {
            _booksLogService = BooksLogService;
        }

        [HttpGet(template: "GetBooksLog")]
        public async Task<ActionResult<IEnumerable<BooksLogDto>>> GetBooksLog()
        {
            IEnumerable<BooksLogDto> BooksLogDtos = await _booksLogService.GetBooksLog();

            return Ok(BooksLogDtos);
        }

        [HttpGet(template: "FindBooksLog/{id}")]

        public async Task<ActionResult<BooksLogDto>> FindBooksLog(int id)
        {
            BooksLogDto BooksLogDto = await _booksLogService.FindBooksLog(id);

            return Ok(BooksLogDto);
        }

        [HttpPost(template: "AddBooksLog")]

        public async Task<ActionResult<string>> AddBooksLog(BooksLog booksLog)
        {
            await _booksLogService.AddBooksLog(booksLog);

            return CreatedAtAction("FindBooksLog", new {id = booksLog.BorrowId}, booksLog);
        }

        [HttpPost(template: "Update/{id}")]

        public async Task<ActionResult> UpdateBooksLog(int id, BooksLog booksLog)
        {
            var result = await _booksLogService.UpdateBooksLog(id, booksLog);

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


        [HttpDelete(template: "DeleteBooksLog/{id}")]
        public async Task<ActionResult> DeleteBooksLog(int id)
        {
            var result = await _booksLogService.DeleteBooksLog(id);

            if (result == "Not Found")
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }
    }
}
