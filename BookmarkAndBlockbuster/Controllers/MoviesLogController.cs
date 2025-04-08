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
    public class MoviesLogController : ControllerBase
    {
        private readonly IMoviesLogService _moviesLogService;

        public MoviesLogController(IMoviesLogService MoviesLogService)
        {
            _moviesLogService = MoviesLogService;
        }

        [HttpGet(template: "GetMoviesLog")]
        public async Task<ActionResult<IEnumerable<MoviesLogDto>>> GetMoviesLog()
        {
            IEnumerable<MoviesLogDto> MoviesLogDtos = await _moviesLogService.GetMoviesLog();

            return Ok(MoviesLogDtos);
        }

        [HttpGet(template: "FindMoviesLog/{id}")]

        public async Task<ActionResult<MoviesLogDto>> FindBooksLog(int id)
        {
            MoviesLogDto MoviesLogDto = await _moviesLogService.FindMoviesLog(id);

            return Ok(MoviesLogDto);
        }

        [HttpPost(template: "AddMoviesLog")]

        public async Task<ActionResult<string>> AddMoviesLog(MoviesLog moviesLog)
        {
            await _moviesLogService.AddMoviesLog(moviesLog);

            return CreatedAtAction("FindMoviesLog", new { id = moviesLog.BorrowId }, moviesLog);
        }

        [HttpPost(template: "Update/{id}")]

        public async Task<ActionResult> UpdateMoviesLog(int id, MoviesLog moviesLog)
        {
            var result = await _moviesLogService.UpdateMoviesLog(id, moviesLog);

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


        [HttpDelete(template: "DeleteMoviesLog/{id}")]
        public async Task<ActionResult> DeleteMoviesLog(int id)
        {
            var result = await _moviesLogService.DeleteMoviesLog(id);

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
