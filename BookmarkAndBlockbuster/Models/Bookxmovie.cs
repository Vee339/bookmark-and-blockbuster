using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookmarkAndBlockbuster.Models
{
    public class Bookxmovie
    {
        [Key]
        public required int Id { get; set; }

        [ForeignKey("Books")]
        public required int BookId { get; set; }

        [ForeignKey("Movies")]
        public required int MovieId { get; set; }
    }

    public class BookxmovieDto
    {
        [Key]
        public required int Id { get; set; }

        public required string BookName { get; set; }

        public required string MovieName { get; set; }
    }
}
