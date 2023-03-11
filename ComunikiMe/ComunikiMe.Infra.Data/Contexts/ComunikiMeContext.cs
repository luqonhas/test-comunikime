using ComunikiMe.Domain.Entities;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace ComunikiMe.Infra.Data.Contexts
{
    public class ComunikiMeContext : DbContext
    {
        public ComunikiMeContext(DbContextOptions<ComunikiMeContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();

            #region Users
            // Adding Table Name
            modelBuilder.Entity<User>().ToTable("Users");

            // Adding Id
            modelBuilder.Entity<User>().Property(x => x.Id);

            // Adding UserName
            modelBuilder.Entity<User>().Property(x => x.UserName).HasColumnType("VARCHAR(50)");
            modelBuilder.Entity<User>().Property(x => x.UserName).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.UserName).IsRequired();

            // Adding Email
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnType("VARCHAR(60)");
            modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(60);
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();

            // Adding Password
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnType("VARCHAR(6)");
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(6);
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();

            // Adding InsertDate
            modelBuilder.Entity<User>().Property(x => x.InsertDate).HasColumnType("DATETIME");
            modelBuilder.Entity<User>().Property(x => x.InsertDate).HasDefaultValueSql("GETDATE()");

            // Adding Data
            //modelBuilder.Entity<User>().HasData(
            //    new User("Lucas Apolinário", "lucas@email.com", "lucas123", Shared.Enums.EnUserType.Administrator),
            //    new User("ComunikiMe", "comunikime@email.com", "comunikime123", Shared.Enums.EnUserType.Client)
            //);
            #endregion

            #region Products
            // Adding Table Name
            modelBuilder.Entity<Product>().ToTable("Products");

            // Adding Id
            modelBuilder.Entity<Product>().Property(x => x.Id);

            // Adding Name
            modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnType("VARCHAR(50)");
            modelBuilder.Entity<Product>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired();

            // Adding Description
            modelBuilder.Entity<Product>().Property(x => x.Description).HasColumnType("VARCHAR(150)");
            modelBuilder.Entity<Product>().Property(x => x.Description).HasMaxLength(150);
            modelBuilder.Entity<Product>().Property(x => x.Description).IsRequired();

            // Adding Price
            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("DECIMAL");
            modelBuilder.Entity<Product>().Property(x => x.Price).HasDefaultValue(0.00);
            modelBuilder.Entity<Product>().Property(x => x.Price).IsRequired();

            // Adding Stock
            modelBuilder.Entity<Product>().Property(x => x.Stock).HasColumnType("INT");
            modelBuilder.Entity<Product>().Property(x => x.Stock).HasDefaultValue(0);
            modelBuilder.Entity<Product>().Property(x => x.Stock).IsRequired();

            // Adding Image
            modelBuilder.Entity<Product>().Property(x => x.Image).HasColumnType("VARCHAR(200)");
            modelBuilder.Entity<Product>().Property(x => x.Image).HasMaxLength(200);
            modelBuilder.Entity<Product>().Property(x => x.Image).HasDefaultValue("https://firebasestorage.googleapis.com/v0/b/podtv-5700.appspot.com/o/upload-image-icon.jpg?alt=media&token=129b5561-51a8-4c2f-81f0-264dcd2397d1");
            modelBuilder.Entity<Product>().Property(x => x.Image).IsRequired();

            // Adding InsertDate
            modelBuilder.Entity<Product>().Property(x => x.InsertDate).HasColumnType("DATETIME");
            modelBuilder.Entity<Product>().Property(x => x.InsertDate).HasDefaultValueSql("GETDATE()");

            // Adding ModifyDate
            modelBuilder.Entity<Product>().Property(x => x.ModifyDate).HasColumnType("DATETIME");
            modelBuilder.Entity<Product>().Property(x => x.ModifyDate).HasDefaultValueSql("GETDATE()");
            #endregion

            #region Carts
            // Adding Table Name
            modelBuilder.Entity<Cart>().ToTable("Carts");

            // Adding Id
            modelBuilder.Entity<Cart>().Property(x => x.Id);

            // Adding User's FK
            modelBuilder.Entity<Cart>()
                        .HasOne(x => x.Users)
                        .WithMany(x => x.Carts)
                        .HasForeignKey(x => x.IdUsers).OnDelete(DeleteBehavior.Restrict);

            // Adding Product's FK
            modelBuilder.Entity<Cart>()
                        .HasOne(x => x.Products)
                        .WithMany(x => x.Carts)
                        .HasForeignKey(x => x.IdProducts).OnDelete(DeleteBehavior.Restrict);

            // Adding InsertDate
            modelBuilder.Entity<Cart>().Property(x => x.InsertDate).HasColumnType("DATETIME");
            modelBuilder.Entity<Cart>().Property(x => x.InsertDate).HasDefaultValueSql("GETDATE()");
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
