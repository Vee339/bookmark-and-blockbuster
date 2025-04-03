using System.ComponentModel.DataAnnotations;

namespace BookmarkAndBlockbuster.Models
{
    public class Hall
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Location { get; set; }

        public required int Capacity { get; set; }

    }
}
