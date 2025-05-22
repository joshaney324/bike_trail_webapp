using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using bike_gps_crud.Models;

namespace bike_gps_crud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<bike_gps_crud.Models.Trail> Trail { get; set; }
        public DbSet<bike_gps_crud.Models.User> User { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trail>()
                .HasOne(t => t.User)
                .WithMany(u => u.Trails)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedNever();
        }
        
       
    }
}
