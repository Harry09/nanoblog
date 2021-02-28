
using Microsoft.EntityFrameworkCore;
using Nanoblog.Api.Data.Models;

namespace Nanoblog.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Entry> Entries { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<EntryKarma> EntryKarma { get; set; }

        public DbSet<CommentKarma> CommentKarma { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
