namespace BookmarkAndBlockbuster.Models.ViewModels
{
    public class MemberDetails
    {
        // A member page must have a Member
        // FindMember(memberid)
        public required Member Member { get; set; }

        // A member may have book logs associated with it
        // GetBooksLogForMember
        public IEnumerable<BooksLogDto>? MemberBooks { get; set; }

        // A member may have movie logs associated to it
        // GetMoviesLogForMember
        public IEnumerable<MoviesLogDto>? MemberMovies { get; set; }
    }
}
