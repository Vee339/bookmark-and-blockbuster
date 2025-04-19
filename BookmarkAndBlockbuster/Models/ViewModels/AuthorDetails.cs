namespace BookmarkAndBlockbuster.Models.ViewModels
{
    public class AuthorDetails
    {
        // An author page must have an Author
        // FindAuthor(authorid)
        public required Author Author { get; set; }

        // An author can have many books associated to it
        // GetBooksForAuthor
        public IEnumerable<Book>? AuthorBooks { get; set; }

        // An authro can have many movies associated to it
        // GetMoviesForAuthor
        public IEnumerable<MovieDto>? AuthorMovies { get; set; }
    }
}
