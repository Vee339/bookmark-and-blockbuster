using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookmarkAndBlockbuster.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        public required string Title {get; set;}

        public string? Genre { get; set; }

        public required int ReleaseYear { get; set; }

        [ForeignKey("Authors")]
        public required int AuthorId { get; set; }
    }
}
