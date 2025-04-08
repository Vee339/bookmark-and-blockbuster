using BookmarkAndBlockbuster.Data.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookmarkAndBlockbuster.Models
{
    public class Screening
    {

        [Key]
        public int ScreeningId { get; set; }

        [ForeignKey("Movie")]
        public int? MovieId { get; set; }

        [ForeignKey("Hall")]
        public int? Id { get; set; }

        public DateOnly ScreeningDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

    }

    public class ScreeningDto
    {
        public int ScreeningId { get; set; }
        public required string Movie { get; set; }

        public required string Hall { get; set; }

        public DateOnly ShowDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
