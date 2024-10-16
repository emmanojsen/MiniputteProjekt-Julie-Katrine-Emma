using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MiniputteProjekt.Model; // Assuming your models are in this namespace

namespace MiniputteProjekt.Data
{
    public class PostsContext : DbContext
    {
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public string DbPath { get; }

        public PostsContext()
        {
            DbPath = "bin/MiniputteProjekt.db"; // Adjusted the database file path
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Posts>().ToTable("Posts");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<Vote>().ToTable("Votes");

            // You can also configure relationships, constraints, etc. here
        }
    }
}
