using BookmarkAndBlockbuster.Models;

namespace BookmarkAndBlockbuster.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> FindBook(int id);
        Task AddBook(Book book);
        Task<string> UpdateBook(int id, Book book);
        Task<string> DeleteBook(int id);
        Task<IEnumerable<Book>> ListBooksForAuthor(int id);
    }
}

