using BookmarkAndBlockbuster.Models;

namespace BookmarkAndBlockbuster.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author?> FindAuthor(int id);
        Task AddAuthor(Author author);
        Task<string> UpdateAuthor(int id, Author author);
        Task<string> DeleteAuthor(int id);
    }
}
