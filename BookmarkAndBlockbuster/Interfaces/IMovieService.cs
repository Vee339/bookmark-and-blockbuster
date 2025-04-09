using BookmarkAndBlockbuster.Models;

namespace BookmarkAndBlockbuster.Interfaces
{
    public interface IMovieService
    {
        public Task<IEnumerable<MovieDto>> GetMovies();

        public Task<MovieDto> FindMovie(int id);

        public Task AddMovie(Movie movie);

        public Task<string> PutMovie(int id, Movie movie);

        public Task<string> DeleteMovie(int id);

        public Task<IEnumerable<MovieDto>> ListMoviesForAuthor(int id);

        public Task<IEnumerable<Movie>> ListMoviesForMember(int id);

    }
}
