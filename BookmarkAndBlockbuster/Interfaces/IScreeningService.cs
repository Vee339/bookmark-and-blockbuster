using BookmarkAndBlockbuster.Models;

namespace BookmarkAndBlockbuster.Interfaces
{
    public interface IScreeningService
    {
        public Task<IEnumerable<ScreeningDto>> GetScreenings();

        public Task<ScreeningDto> FindScreening(int id);

        public Task AddScreening(Screening Screening);

        public Task<string> EditScreening(int id, Screening screening);

        public Task<string> DeleteScreening(int id);

        public Task<IEnumerable<ScreeningDto>> ListScreeningsForMovie(int id);

        public Task<IEnumerable<ScreeningDto>> ListScreeningsForHall(int id);

    }
}
