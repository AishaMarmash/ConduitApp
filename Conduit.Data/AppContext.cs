using Microsoft.EntityFrameworkCore;
using Conduit.Domain.Entities;

namespace Conduit.Data
{
    public class AppContext : DbContext
    {
        public AppContext()
        { }
        public AppContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(@"Data Source=DESKTOP-ICHCNJM\SQLEXPRESS;Initial Catalog = ConduitData;Integrated Security=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<User>()
            //    .HasMany(u => u.Articles)
            //    .WithOne();

            //builder.Entity<FollowingUsers>()
            //    .HasKey(u => new { u.UserId, u.FollowingUserId });

            //builder.Entity<FollowingUsers>()
            //    .HasOne(fu => fu.User)
            //    .WithMany(u => u.FollowingUsers)
            //    .HasForeignKey(fu => fu.UserId);

            //builder.Entity<FollowingUsers>()
            //    .HasOne(fu => fu.User)
            //    .WithMany(u => u.FollowingUsers)
            //    .HasForeignKey(fu => fu.FollowingUserId);
                
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }


    }
}
