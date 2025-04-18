using BookmarkAndBlockbuster.Models;

namespace BookmarkAndBlockbuster.Interfaces
{
    public interface IBooksLogService
    {
        public Task<IEnumerable<BooksLogDto>> GetBooksLog();

        public Task<BooksLogDto> FindBooksLog(int id);

        public Task AddBooksLog(BooksLog booksLog);

        public Task<string> UpdateBooksLog(int id, BooksLog booksLog);

        public Task<string> DeleteBooksLog(int id);

        public Task<IEnumerable<BooksLogDto>> GetBooksLogForMember(int id);

        public Task<IEnumerable<BooksLogDto>> GetBooksLogForBook(int id);
    }
}
