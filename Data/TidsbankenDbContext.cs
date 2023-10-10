using System;
using Microsoft.EntityFrameworkCore;
using Tidsbanken_BackEnd.Data.Entities;
using Tidsbanken_BackEnd.Data.Enums;

namespace Tidsbanken_BackEnd.Data
{
    public class TidsbankenDbContext : DbContext
    {
        public TidsbankenDbContext(DbContextOptions<TidsbankenDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<IneligiblePeriod> IneligiblePeriods { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationships
            // User one to many VacationRequest : Makes
            modelBuilder.Entity<VacationRequest>()
                .HasOne(v => v.User)
                .WithMany(u => u.VacationRequests)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            // User one to many IneligiblePeriod : Has
            modelBuilder.Entity<IneligiblePeriod>()
                .HasOne(ip => ip.User)
                .WithMany(u => u.IneligiblePeriods)
                .HasForeignKey(ip => ip.UserId);

            // Role one to many User : Has
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction); // Change this to NoAction;

            // VacationRequest one to many Comments : Has
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.VacationRequest)
                .WithMany(vr => vr.Comments)
                .HasForeignKey(c => c.VacationRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // VacationRequest one to one VacationRequestStatus : Has
            modelBuilder.Entity<VacationRequest>()
                .Property(vr => vr.Status)
                .HasConversion<String>();


            // Seeding Data
            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Employee" },
                new Role { Id = 2, RoleName = "Admin" },
                new Role { Id = 3, RoleName = "Manager" }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "employee1",
                    FirstName = "John",
                    LastName = "Doe",
                    Password = "hashed_password1", // Use a secure password hashing method
                    Email = "john.doe@example.com",
                    RoleId = 1 // Employee
                },
                new User
                {
                    Id = 2,
                    Username = "employee2",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Password = "hashed_password2",
                    Email = "jane.smith@example.com",
                    RoleId = 1 // Employee
                },
                new User
                {
                    Id = 3,
                    Username = "admin1",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Password = "hashed_password3",
                    Email = "admin@example.com",
                    RoleId = 2 // Admin
                },
                new User
                {
                    Id = 4,
                    Username = "manager1",
                    FirstName = "Manager",
                    LastName = "Manager",
                    Password = "hashed_password4",
                    Email = "manager@example.com",
                    RoleId = 3 // Manager
                },
                new User
                {
                    Id = 5,
                    Username = "employee3",
                    FirstName = "Sarah",
                    LastName = "Johnson",
                    Password = "hashed_password5",
                    Email = "sarah.johnson@example.com",
                    RoleId = 1 // Employee
                }
            );

            // Seed VacationRequests
            modelBuilder.Entity<VacationRequest>().HasData(
                new VacationRequest
                {
                    Id = 1,
                    StartDate = DateTime.Now.Date,
                    EndDate = DateTime.Now.Date.AddDays(5),
                    Status = VacationRequestStatus.Pending,
                    UserId = 1, // User with Employee role
                    RequestDate = DateTime.Now
                },
                new VacationRequest
                {
                    Id = 2,
                    StartDate = DateTime.Now.Date.AddMonths(1),
                    EndDate = DateTime.Now.Date.AddMonths(1).AddDays(10),
                    Status = VacationRequestStatus.Approved,
                    UserId = 2, // User with Employee role
                    RequestDate = DateTime.Now.AddMonths(1)
                },
                new VacationRequest
                {
                    Id = 3,
                    StartDate = DateTime.Now.Date.AddMonths(2),
                    EndDate = DateTime.Now.Date.AddMonths(2).AddDays(7),
                    Status = VacationRequestStatus.Pending,
                    UserId = 3, // User with Manager role
                    RequestDate = DateTime.Now.AddMonths(2)
                },
                new VacationRequest
                {
                    Id = 4,
                    StartDate = DateTime.Now.Date.AddMonths(1).AddDays(15),
                    EndDate = DateTime.Now.Date.AddMonths(1).AddDays(20),
                    Status = VacationRequestStatus.Approved,
                    UserId = 4, // User with Manager role
                    RequestDate = DateTime.Now.AddMonths(1).AddDays(15)
                },
                new VacationRequest
                {
                    Id = 5,
                    StartDate = DateTime.Now.Date.AddMonths(3),
                    EndDate = DateTime.Now.Date.AddMonths(3).AddDays(5),
                    Status = VacationRequestStatus.Pending,
                    UserId = 5, // User with Employee role
                    RequestDate = DateTime.Now.AddMonths(3)
                }
            );

            // Seed IneligiblePeriods
            modelBuilder.Entity<IneligiblePeriod>().HasData(
                new IneligiblePeriod
                {
                    Id = 1,
                    StartDate = DateTime.Now.Date.AddMonths(1),
                    EndDate = DateTime.Now.Date.AddMonths(2),
                    Description = "Vacation blackout period 1",
                    UserId = 3 // User with Manager role
                },
                new IneligiblePeriod
                {
                    Id = 2,
                    StartDate = DateTime.Now.Date.AddMonths(4),
                    EndDate = DateTime.Now.Date.AddMonths(5),
                    Description = "Vacation blackout period 2",
                    UserId = 4 // User with Manager role
                },
                new IneligiblePeriod
                {
                    Id = 3,
                    StartDate = DateTime.Now.Date.AddMonths(7),
                    EndDate = DateTime.Now.Date.AddMonths(8),
                    Description = "Vacation blackout period 3",
                    UserId = 4 // User with Manager role
                },
                new IneligiblePeriod
                {
                    Id = 4,
                    StartDate = DateTime.Now.Date.AddMonths(2).AddDays(10),
                    EndDate = DateTime.Now.Date.AddMonths(2).AddDays(20),
                    Description = "Vacation blackout period 4",
                    UserId = 3 // User with Manager role
                },
                new IneligiblePeriod
                {
                    Id = 5,
                    StartDate = DateTime.Now.Date.AddMonths(1),
                    EndDate = DateTime.Now.Date.AddMonths(1).AddDays(5),
                    Description = "Vacation blackout period 5",
                    UserId = 2 // User with Employee role
                }
            );

            // Seed Comments
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = 1,
                    Message = "This is a comment by John.",
                    DateCommented = DateTime.Now,
                    StatusAtTimeOfComment = VacationRequestStatus.Pending,
                    VacationRequestId = 1 // VacationRequest ID
                },
                new Comment
                {
                    Id = 2,
                    Message = "This is a comment by Manager.",
                    DateCommented = DateTime.Now,
                    StatusAtTimeOfComment = VacationRequestStatus.Approved,
                    VacationRequestId = 2 // VacationRequest ID
                },
                new Comment
                {
                    Id = 3,
                    Message = "Another comment by Manager.",
                    DateCommented = DateTime.Now,
                    StatusAtTimeOfComment = VacationRequestStatus.Pending,
                    VacationRequestId = 3 // VacationRequest ID
                },
                new Comment
                {
                    Id = 4,
                    Message = "A comment by Admin.",
                    DateCommented = DateTime.Now,
                    StatusAtTimeOfComment = VacationRequestStatus.Approved,
                    VacationRequestId = 4 // VacationRequest ID
                },
                new Comment
                {
                    Id = 5,
                    Message = "A comment by Jane.",
                    DateCommented = DateTime.Now,
                    StatusAtTimeOfComment = VacationRequestStatus.Pending,
                    VacationRequestId = 5 // VacationRequest ID
                }
            );
        }
    }
}