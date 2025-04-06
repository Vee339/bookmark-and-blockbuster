using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Data;
using BookmarkAndBlockbuster.Interfaces;
using Microsoft.EntityFrameworkCore;
using BookmarkAndBlockbuster.Data.Migrations;

namespace BookmarkAndBlockbuster.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            List<Author> authors = await _context.Authors.ToListAsync();
            return authors;
        }

        public async Task<Author?> FindAuthor(int id)
        {
            Author? Author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);
            if (Author == null)
            {
                return null;
            }
            return Author;
        }

        public async Task AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task<string> UpdateAuthor(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return "Bad Request";
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        public async Task<string> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return "Not Found";
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return "No Content";
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(a => a.AuthorId == id);
        }
    }
}
