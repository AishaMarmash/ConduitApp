using Conduit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel;

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
