using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Data;
using BookmarkAndBlockbuster.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookmarkAndBlockbuster.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            List<Book> books = await _context.Books.ToListAsync();
            return books;
        }

        public async Task<Book?> FindBook(int id)
        {
            Book? book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return null;
            }

            return book;
        }

        public async Task AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<string> UpdateBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return "Bad Request";
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        public async Task<string> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return "Not Found";
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return "No Content";
        }

        public async Task<IEnumerable<Book>> ListBooksForAuthor(int id)
        {
            List<Book> books = await _context.Books.Include(b => b.Author).Where(b => b.AuthorId == id).ToListAsync();
            return books;
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(b => b.BookId == id);
        }
    }
}
