using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Data;
using BookmarkAndBlockbuster.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BookmarkAndBlockbuster.Services
{
    public class ScreeningService : IScreeningService
    {
        private readonly ApplicationDbContext _context;

        public ScreeningService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ScreeningDto>> GetScreenings()
        {
            List<Screening> Screenings = await _context.Screenings.Include(s => s.Movie).Include(s => s.Hall).ToListAsync();

            List<ScreeningDto> ScreeningDtos = new List<ScreeningDto>();

            foreach (Screening Screening in Screenings)
            {
                ScreeningDto ScreeningDto = new ScreeningDto
                {
                    ScreeningId = Screening.ScreeningId,
                    Movie = Screening.Movie.Title,
                    Hall = Screening.Hall.Name + " " + Screening.Hall.Location,
                    ShowDate = Screening.ScreeningDate,
                    StartTime = Screening.StartTime,
                    EndTime = Screening.EndTime,
                };

                ScreeningDtos.Add(ScreeningDto);

            }

            return ScreeningDtos;
        }

        public async Task<ScreeningDto> FindScreening(int id)
        {
            Screening Screening = await _context.Screenings.Include(s => s.Movie).Include(s => s.Hall).Where(s => s.ScreeningId == id).FirstOrDefaultAsync();

            ScreeningDto ScreeningDto = new ScreeningDto
            {
                ScreeningId = Screening.ScreeningId,
                Movie = Screening.Movie.Title,
                Hall = Screening.Hall.Name + " " + Screening.Hall.Location,
                ShowDate = Screening.ScreeningDate,
                StartTime = Screening.StartTime,
                EndTime = Screening.EndTime
            };
            return ScreeningDto;
        }

        public async Task AddScreening(Screening screening)
        {
            _context.Screenings.Add(screening);

            await _context.SaveChangesAsync();

        }

        public async Task<string> EditScreening(int id, Screening screening)
        {
            if (id != screening.ScreeningId)
            {
                return "Bad Request";
            }

            _context.Entry(screening).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreeningExists(id))
                {
                    return "Not Found";
                }
                else
                {
                    throw;
                }

            }

            return "No Content";
        }

        public async Task<string> DeleteScreening(int id)
        {
            var screening = await _context.Screenings.FindAsync(id);

            if (screening == null)
            {
                return "Not Found";
            }

            _context.Screenings.Remove(screening);

            await _context.SaveChangesAsync();

            return "No Content";
        }

        public async Task<IEnumerable<ScreeningDto>> ListScreeningsForMovie(int id)
        {
            List<Screening> Screenings = await _context.Screenings.Include(s => s.Movie).Include(s => s.Hall).Where(s => s.MovieId == id).ToListAsync();

            List<ScreeningDto> ScreeningDtos = new List<ScreeningDto>();

            foreach (Screening Screening in Screenings)
            {
                ScreeningDto ScreeningDto = new ScreeningDto
                {
                    ScreeningId = Screening.ScreeningId,
                    Movie = Screening.Movie.Title,
                    Hall = Screening.Hall.Name + " " + Screening.Hall.Location,
                    ShowDate = Screening.ScreeningDate,
                    StartTime = Screening.StartTime,
                    EndTime = Screening.EndTime
                };
                ScreeningDtos.Add(ScreeningDto);
            }

            return ScreeningDtos;
        }

        public async Task<IEnumerable<ScreeningDto>> ListScreeningsForHall(int id)
        {
            List<Screening> Screenings = await _context.Screenings.Include(s => s.Movie).Include(s => s.Hall).Where(s => s.Id == id).ToListAsync();

            List<ScreeningDto> ScreeningDtos = new List<ScreeningDto>();

            foreach (Screening Screening in Screenings)
            {
                ScreeningDto ScreeningDto = new ScreeningDto
                {
                    ScreeningId = Screening.ScreeningId,
                    Movie = Screening.Movie.Title,
                    Hall = Screening.Hall.Name + " " + Screening.Hall.Location,
                    ShowDate = Screening.ScreeningDate,
                    StartTime = Screening.StartTime,
                    EndTime = Screening.EndTime
                };

                

                ScreeningDtos.Add(ScreeningDto);
            }

            return ScreeningDtos;
        }
        private bool ScreeningExists(int id)
        {
            return _context.Screenings.Any(e => e.ScreeningId == id);
        }
    }
}
