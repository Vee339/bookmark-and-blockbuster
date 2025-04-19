namespace BookmarkAndBlockbuster.Models.ViewModels
{
    public class BookDetails
    {
        // A book page must have book information
        // FindBook(bookid)
        public required Book Book { get; set; }

        // A book may have bookslog associated with it
        public IEnumerable<BooksLogDto>? BooksLog { get; set; }
    }
}
