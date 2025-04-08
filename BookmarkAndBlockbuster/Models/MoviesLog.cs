using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookmarkAndBlockbuster.Models
{
    public class MoviesLog
    {
        [Key]
        public int BorrowId { get; set; }

        [ForeignKey("Members")]
        public required int MemberId { get; set; }

        public virtual Member? Member { get; set; }

        [ForeignKey("Movies")]
        public required int MovieId { get; set; }
        public virtual Movie? Movie { get; set; }

        public required DateOnly BorrowDate { get; set; }

        public DateOnly? DueDate { get; set; }
        public DateOnly? ReturnDate { get; set; }

    }

    public class MoviesLogDto
    {
        public int? BorrowId { get; set; }

        public required string MemberName { get; set; }

        public required string MovieName { get; set; }

        public required DateOnly BorrowDate { get; set; }

        public DateOnly? DueDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
    }
}


  