using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookmarkAndBlockbuster.Models
{
    public class BooksLog
    {
        [Key]
        public int BorrowId { get; set; }

        [ForeignKey("Members")]
        public required int MemberId { get; set; }

        [ForeignKey("Books")]
        public required int BookId { get; set; }

        public required DateOnly BorrowDate { get; set; }

        public DateOnly? DueDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
    }

    public class BooksLogDto
    {
        public int? BorrowId { get; set; }

        public required string MemberName { get; set; }

        public required string BookName { get; set; }

        public required DateOnly BorrowDate { get; set; }

        public DateOnly? DueDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
    }
}
