using Microsoft.EntityFrameworkCore;
using Conduit.Domain.Entities;

namespace Conduit.Data
{
    public class AppContext : DbContext
    {
        public AppContext(){ }
        public AppContext(DbContextOptions options) : base(options){ }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(@"Data Source=DESKTOP-ICHCNJM\SQLEXPRESS;Initial Catalog = ConduitData;Integrated Security=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
            .Entity<User>()
            .HasMany(u => u.Articles)
            .WithOne(u => u.User);
            
            builder
            .Entity<User>()
            .HasMany(u => u.FavoritedArticles)
            .WithMany(u => u.FavoritesUsers)
            .UsingEntity(j => j.ToTable("UsersFavoriteArticles"));

            builder
           .Entity<Article>()
           .HasMany(u => u.Comments)
           .WithOne(u => u.Article);

            builder
           .Entity<User>()
           .HasMany(u => u.Comments)
           .WithOne(u => u.Author);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}