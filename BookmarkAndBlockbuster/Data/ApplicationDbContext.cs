using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookmarkAndBlockbuster.Models;

namespace BookmarkAndBlockbuster.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Member> Members { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
