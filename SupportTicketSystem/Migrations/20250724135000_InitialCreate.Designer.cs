﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SupportTicketSystem.Data;

#nullable disable

namespace SupportTicketSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250724135000_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.7");

            modelBuilder.Entity("SupportTicketSystem.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRead")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SenderName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TicketId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("SupportTicketSystem.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AssignedToUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AttachmentPath")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Priority")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AssignedToUserId");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("SupportTicketSystem.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FullName = "مدیر سیستم",
                            Password = "admin1234",
                            Role = "Admin",
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            FullName = "رها آسایش",
                            Password = "userit1234",
                            Role = "IT",
                            Username = "userit1"
                        },
                        new
                        {
                            Id = 3,
                            FullName = "امید پناهی",
                            Password = "userit4321",
                            Role = "IT",
                            Username = "userit2"
                        },
                        new
                        {
                            Id = 4,
                            FullName = "رضا اسدی",
                            Password = "asadi1234",
                            Role = "Employee",
                            Username = "asadi"
                        },
                        new
                        {
                            Id = 5,
                            FullName = "فرزانه محمدی",
                            Password = "mohammadi1234",
                            Role = "Employee",
                            Username = "mohammadi"
                        },
                        new
                        {
                            Id = 6,
                            FullName = "محمد رضایی",
                            Password = "rezaei1234",
                            Role = "Employee",
                            Username = "rezaei"
                        });
                });

            modelBuilder.Entity("SupportTicketSystem.Models.Notification", b =>
                {
                    b.HasOne("SupportTicketSystem.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SupportTicketSystem.Models.Ticket", b =>
                {
                    b.HasOne("SupportTicketSystem.Models.User", "AssignedToUser")
                        .WithMany()
                        .HasForeignKey("AssignedToUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SupportTicketSystem.Models.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AssignedToUser");

                    b.Navigation("CreatedByUser");
                });
#pragma warning restore 612, 618
        }
    }
}
