using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NLP_Delivery.Models;

namespace NLP_Delivery.Data
{
    public class DataContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)        {        }

        public DbSet<Brands> Brands { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<Stores> Stores { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<UniformDispensing> UniformDispensing { get; set; }
        //public DbSet<Users> Users { get; set; }
        public DbSet<UsersHistory> UsersHistory { get; set; }
        //public DbSet<Roles> Roles { get; set; }
        public DbSet<Receivers> Receivers { get; set; }

        public DbSet<Badges> Badges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UniformDispensing>()
                .HasOne(u => u.Store)
                .WithMany()
                .HasForeignKey(u => u.StoreID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UniformDispensing>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UniformDispensing>()
                .HasOne(u => u.Receiver)
                .WithMany()
                .HasForeignKey(u => u.ReceiverID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UniformDispensing>()
                .HasOne(u => u.Product)
                .WithMany()
                .HasForeignKey(u => u.ProductID)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}

