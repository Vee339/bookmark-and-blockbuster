namespace BookmarkAndBlockbuster.Models.ViewModels
{
    public class EditBookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}
