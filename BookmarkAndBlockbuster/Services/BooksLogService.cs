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
    public class BooksLogService : IBooksLogService
    {
        private readonly ApplicationDbContext _context;

        public BooksLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BooksLogDto>> GetBooksLog()
        {
            List<BooksLog> BooksLogs = await _context.BooksLogs.Include(bl => bl.Member).Include(bl => bl.Book).ToListAsync();

            List<BooksLogDto> BooksLogDtos = new List<BooksLogDto>();

            foreach (BooksLog BooksLog in BooksLogs)
            {
                BooksLogDto BooksLogDto = new BooksLogDto
                {
                    BorrowId = BooksLog.BorrowId,
                    MemberName = BooksLog.Member.MemberName,
                    BookName = BooksLog.Book.BookTitle,
                    BorrowDate = BooksLog.BorrowDate,
                    DueDate = BooksLog.DueDate,
                    ReturnDate = BooksLog.ReturnDate,
                };

                BooksLogDtos.Add(BooksLogDto);
            }
            return BooksLogDtos;
        }

        public async Task<BooksLogDto> FindBooksLog(int id)
        {
            BooksLog BooksLog = await _context.BooksLogs.Include(bl => bl.Member).Include(bl => bl.Book).Where(bl => bl.BorrowId == id).FirstOrDefaultAsync();

            BooksLogDto BooksLogDto = new BooksLogDto
            {
                BorrowId = BooksLog.BorrowId,
                MemberName = BooksLog.Member.MemberName,
                BookName = BooksLog.Book.BookTitle,
                BorrowDate = BooksLog.BorrowDate,
                DueDate = BooksLog.DueDate,
                ReturnDate = BooksLog.ReturnDate,
            };

            return BooksLogDto;
        }

        public async Task AddBooksLog(BooksLog booksLog)
        {
            _context.BooksLogs.Add(booksLog);

            await _context.SaveChangesAsync();
        }

        public async Task<string> UpdateBooksLog(int id, BooksLog booksLog)
        {
            if (id != booksLog.BorrowId)
            {
                return "Bad Request";
            }

            _context.Entry(booksLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksLogExists(id))
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

        public async Task<string> DeleteBooksLog(int id)
        {
            var booksLog = await _context.BooksLogs.FindAsync(id);

            if (booksLog == null)
            {
                return "Not Found";
            }

            _context.BooksLogs.Remove(booksLog);

            await _context.SaveChangesAsync();

            return "No Content";
        }

        public async Task<IEnumerable<BooksLogDto>> GetBooksLogForMember(int id)
        {
            List<BooksLog> BooksLogs = await _context.BooksLogs.Include(bl => bl.Member).Include(bl => bl.Book).Where(bl => bl.MemberId == id).ToListAsync();

            List<BooksLogDto> BooksLogDtos = new List<BooksLogDto>();

            foreach (BooksLog BooksLog in BooksLogs)
            {
                BooksLogDto BooksLogDto = new BooksLogDto
                {
                    BorrowId = BooksLog.BorrowId,
                    MemberName = BooksLog.Member.MemberName,
                    BookName = BooksLog.Book.BookTitle,
                    BorrowDate = BooksLog.BorrowDate,
                    DueDate = BooksLog.DueDate,
                    ReturnDate = BooksLog.ReturnDate,
                };

                BooksLogDtos.Add(BooksLogDto);
            }
            return BooksLogDtos;
        }

        public async Task<IEnumerable<BooksLogDto>> GetBooksLogForBook(int id)
        {
            List<BooksLog> BooksLogs = await _context.BooksLogs.Include(bl => bl.Member).Include(bl => bl.Book).Where(bl => bl.BookId == id).ToListAsync();

            List<BooksLogDto> BooksLogDtos = new List<BooksLogDto>();

            foreach (BooksLog BooksLog in BooksLogs)
            {
                BooksLogDto BooksLogDto = new BooksLogDto
                {
                    BorrowId = BooksLog.BorrowId,
                    MemberName = BooksLog.Member.MemberName,
                    BookName = BooksLog.Book.BookTitle,
                    BorrowDate = BooksLog.BorrowDate,
                    DueDate = BooksLog.DueDate,
                    ReturnDate = BooksLog.ReturnDate,
                };

                BooksLogDtos.Add(BooksLogDto);
            }
            return BooksLogDtos;
        }

        private bool BooksLogExists(int id)
        {
            return _context.BooksLogs.Any(e => e.BorrowId == id);
        }
    }
}
