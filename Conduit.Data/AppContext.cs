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

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*base.OnModelCreating(builder);
            builder.Entity<User>()
            .HasOne<Profile>(p=>p.Id)
            .WithOne(u => u.User)
            .HasForeignKey(p => p.CategoryId);*/

        }
    }
}
