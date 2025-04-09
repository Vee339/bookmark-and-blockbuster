using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookmarkAndBlockbuster.Models
{
    public class Book
    {
      [Key]
      public int BookId { get; set; }

      public required string BookTitle { get; set; }

      [ForeignKey("Author")]
      public required int AuthorId { get; set; }
      public virtual Author? Author { get; set; }

      public int? PublishedYear { get; set; }

      public required string Genre { get; set; }
     }
    
}
