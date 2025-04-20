namespace BookmarkAndBlockbuster.Models.ViewModels
{
    public class HallDetails
    {
        public required Hall Hall { get; set; }

        public IEnumerable<ScreeningDto>? Screenings { get; set; }
    }
}
