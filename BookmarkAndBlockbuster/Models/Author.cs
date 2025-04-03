using System.ComponentModel.DataAnnotations;

namespace BookmarkAndBlockbuster.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        public required string AuthorName { get; set; }

        public string? AuthorDescription { get; set; }
    }
}
