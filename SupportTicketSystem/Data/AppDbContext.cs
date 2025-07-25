﻿using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportTicketSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تنظیم صریح AUTOINCREMENT برای SQLite
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Ticket>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Notification>()
                .Property(n => n.Id)
                .ValueGeneratedOnAdd();

            // روابط بین جداول
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.CreatedByUser)
                .WithMany()
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AssignedToUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // داده اولیه
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "مدیر سیستم", Username = "admin", Password = "admin1234", Role = "Admin" },
                new User { Id = 2, FullName = "رها آسایش", Username = "userit1", Password = "userit1234", Role = "IT" },
                new User { Id = 3, FullName = "امید پناهی", Username = "userit2", Password = "userit4321", Role = "IT" },
                new User { Id = 4, FullName = "رضا اسدی", Username = "asadi", Password = "asadi1234", Role = "Employee" },
                new User { Id = 5, FullName = "فرزانه محمدی", Username = "mohammadi", Password = "mohammadi1234", Role = "Employee" },
                new User { Id = 6, FullName = "محمد رضایی", Username = "rezaei", Password = "rezaei1234", Role = "Employee" }
            );
        }
    }
}
