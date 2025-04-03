using System.ComponentModel.DataAnnotations;

namespace BookmarkAndBlockbuster.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        public required string MemberName { get; set; }

        public DateOnly? BirthDate { get; set; }

        public required string MemberPhone { get; set; }

        public string? MemberEmail { get; set; }
    }
}
