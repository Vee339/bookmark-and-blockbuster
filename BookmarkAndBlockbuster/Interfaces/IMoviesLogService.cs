using BookmarkAndBlockbuster.Models;

namespace BookmarkAndBlockbuster.Interfaces
{
    public interface IMoviesLogService
    {
        public Task<IEnumerable<MoviesLogDto>> GetMoviesLog();

        public Task<MoviesLogDto> FindMoviesLog(int id);

        public Task AddMoviesLog(MoviesLog moviesLog);

        public Task<string> UpdateMoviesLog(int id, MoviesLog moviesLog);

        public Task<string> DeleteMoviesLog(int id);

        public Task<IEnumerable<MoviesLogDto>> GetMoviesLogForMember(int id);

        public Task<IEnumerable<MoviesLogDto>> GetMoviesLogForMovie(int id);
    }
}
