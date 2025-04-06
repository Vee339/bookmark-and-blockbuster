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

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<BooksLog> BooksLogs { get; set; }

        public DbSet<MoviesLog> MoviesLogs { get; set; }

        public DbSet<Bookxmovie> Booksxmovies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}