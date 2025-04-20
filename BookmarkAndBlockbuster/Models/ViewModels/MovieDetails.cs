namespace BookmarkAndBlockbuster.Models.ViewModels
{
    public class MovieDetails
    {

        public required MovieDto Movie { get; set; }


        public IEnumerable<ScreeningDto>? Screenings { get; set; }

        public IEnumerable<Author>? Authors { get; set; }

    }
}
