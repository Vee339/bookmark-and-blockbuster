using BookmarkAndBlockbuster.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Services;
using BookmarkAndBlockbuster.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;
using BookmarkAndBlockbuster.Data.Migrations;

namespace BookmarkAndBlockbuster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningController : ControllerBase
    {
        private readonly IScreeningService _screeningService;

        public ScreeningController(IScreeningService ScreeningService)
        {
            _screeningService = ScreeningService;
        }

        [HttpGet(template: "GetScreenings")]

        public async Task<ActionResult<IEnumerable<ScreeningDto>>> GetScreenings()
        {
            IEnumerable<ScreeningDto> Screenings = await _screeningService.GetScreenings();

            return Ok(Screenings);
        }

        [HttpGet(template: "FindScreening/{id}")]

        public async Task<ActionResult<ScreeningDto>> FindScreening(int id)
        {
            ScreeningDto Screening = await _screeningService.FindScreening(id);

            return Ok(Screening);
        }

        [HttpPost(template: "AddScreening")]
        public async Task<ActionResult<string>> AddScreening(Screening screening)
        {
            await _screeningService.AddScreening(screening);

            return CreatedAtAction("FindScreening", new { id = screening.ScreeningId }, screening);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditScreening(int id, Screening screening)
        {
            var result = await _screeningService.EditScreening(id, screening);

            if (result == "Bad Request")
            {
                return BadRequest();
            }
            else if (result == "Not Found")
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }

        [HttpDelete(template: "DeleteScreening/{id}")]
        public async Task<ActionResult> DeleteScreening(int id)
        {
            var result = await _screeningService.DeleteScreening(id);

            if (result == "Not Found")
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet(template: "ListScreeningsForMovie/{id}")]

        public async Task<ActionResult<IEnumerable<ScreeningDto>>> ListScreeningsForMovie(int id)
        {

            IEnumerable<ScreeningDto> ScreeningDtos = await _screeningService.ListScreeningsForMovie(id);


            return Ok(ScreeningDtos);
        }

        [HttpGet(template: "ListScreeningsForHall/{id}")]

        public async Task<ActionResult<IEnumerable<ScreeningDto>>> ListScreeningsForHall(int id)
        {

            IEnumerable<ScreeningDto> ScreeningDtos = await _screeningService.ListScreeningsForHall(id);

            return Ok(ScreeningDtos);
        }
    }
}