using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Interfaces;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Numerics;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using BookmarkAndBlockbuster.Data.Migrations;

namespace BookmarkAndBlockbuster.Services
{
    public class MoviesLogService : IMoviesLogService
    {
        private readonly ApplicationDbContext _context;

        public MoviesLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MoviesLogDto>> GetMoviesLog()
        {
            List<MoviesLog> MoviesLogs = await _context.MoviesLogs.Include(ml => ml.Member).Include(ml => ml.Movie).ToListAsync();

            List<MoviesLogDto> MoviesLogDtos = new List<MoviesLogDto>();

            foreach (MoviesLog MoviesLog in MoviesLogs)
            {
                MoviesLogDto MoviesLogDto = new MoviesLogDto
                {
                    BorrowId = MoviesLog.BorrowId,
                    MemberName = MoviesLog.Member.MemberName,
                    MovieName = MoviesLog.Movie.Title,
                    BorrowDate = MoviesLog.BorrowDate,
                    DueDate = MoviesLog.DueDate,
                    ReturnDate = MoviesLog.ReturnDate,
                };

                MoviesLogDtos.Add(MoviesLogDto);
            }
            return MoviesLogDtos;
        }

        public async Task<MoviesLogDto> FindMoviesLog(int id)
        {
            MoviesLog MoviesLog = await _context.MoviesLogs.Include(ml => ml.Member).Include(ml => ml.Movie).Where(ml => ml.BorrowId == id).FirstOrDefaultAsync();

            MoviesLogDto MoviesLogDto = new MoviesLogDto
            {
                BorrowId = MoviesLog.BorrowId,
                MemberName = MoviesLog.Member.MemberName,
                MovieName = MoviesLog.Movie.Title,
                BorrowDate = MoviesLog.BorrowDate,
                DueDate = MoviesLog.DueDate,
                ReturnDate = MoviesLog.ReturnDate,
            };

            return MoviesLogDto;
        }

        public async Task AddMoviesLog(MoviesLog moviesLog)
        {
            _context.MoviesLogs.Add(moviesLog);

            await _context.SaveChangesAsync();
        }

        public async Task<string> UpdateMoviesLog(int id, MoviesLog moviesLog)
        {
            if (id != moviesLog.BorrowId)
            {
                return "Bad Request";
            }

            _context.Entry(moviesLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesLogExists(id))
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

        public async Task<string> DeleteMoviesLog(int id)
        {
            var moviesLog = await _context.MoviesLogs.FindAsync(id);

            if (moviesLog == null)
            {
                return "Not Found";
            }

            _context.MoviesLogs.Remove(moviesLog);

            await _context.SaveChangesAsync();

            return "No Content";
        }

        private bool MoviesLogExists(int id)
        {
            return _context.MoviesLogs.Any(e => e.BorrowId == id);
        }
    }
}
