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

            // VacationRequest one to one VacationType : Has
            modelBuilder.Entity<VacationRequest>()
                .Property(vr => vr.VacationType)
                .HasConversion<String>();


            // Seeding Data
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = new Guid("6786c233-5f89-4e5e-af84-2ff7db03ba86"),
                    Username = "admin",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@admin.dk"
                },
                new User
                {
                    Id = new Guid("7d94f7d7-da61-49a0-b0e3-8790b93168de"),
                    Username = "employee",
                    FirstName = "Employee",
                    LastName = "Employee",
                    Email = "employee@employee.dk"
                }
            );

            // Seed VacationRequests
            modelBuilder.Entity<VacationRequest>().HasData(
                new VacationRequest
                {
                    Id = 1,
                    VacationType = VacationType.Vacation,
                    StartDate = DateTime.Now.Date,
                    EndDate = DateTime.Now.Date.AddDays(5),
                    Status = VacationRequestStatus.Pending,
                    UserId = new Guid("7d94f7d7-da61-49a0-b0e3-8790b93168de"), // User with Employee role
                    RequestDate = DateTime.Now
                },
                new VacationRequest
                {
                    Id = 2,
                    VacationType = VacationType.Vacation,
                    StartDate = DateTime.Now.Date.AddMonths(1),
                    EndDate = DateTime.Now.Date.AddMonths(1).AddDays(10),
                    Status = VacationRequestStatus.Approved,
                    UserId = new Guid("7d94f7d7-da61-49a0-b0e3-8790b93168de"), // User with Employee role
                    RequestDate = DateTime.Now.AddMonths(1)
                },
                new VacationRequest
                {
                    Id = 3,
                    VacationType = VacationType.Vacation,
                    StartDate = DateTime.Now.Date.AddMonths(2),
                    EndDate = DateTime.Now.Date.AddMonths(2).AddDays(7),
                    Status = VacationRequestStatus.Pending,
                    UserId = new Guid("6786c233-5f89-4e5e-af84-2ff7db03ba86"), // User with Admin role
                    RequestDate = DateTime.Now.AddMonths(2)
                },
                new VacationRequest
                {
                    Id = 4,
                    VacationType = VacationType.Vacation,
                    StartDate = DateTime.Now.Date.AddMonths(1).AddDays(15),
                    EndDate = DateTime.Now.Date.AddMonths(1).AddDays(20),
                    Status = VacationRequestStatus.Approved,
                    UserId = new Guid("6786c233-5f89-4e5e-af84-2ff7db03ba86"), // User with Admin role
                    RequestDate = DateTime.Now.AddMonths(1).AddDays(15)
                },
                new VacationRequest
                {
                    Id = 5,
                    VacationType = VacationType.Vacation,
                    StartDate = DateTime.Now.Date.AddMonths(3),
                    EndDate = DateTime.Now.Date.AddMonths(3).AddDays(5),
                    Status = VacationRequestStatus.Pending,
                    UserId = new Guid("7d94f7d7-da61-49a0-b0e3-8790b93168de"), // User with Employee role
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
                    UserId = new Guid("6786c233-5f89-4e5e-af84-2ff7db03ba86") // User with Admin role
                },
                new IneligiblePeriod
                {
                    Id = 2,
                    StartDate = DateTime.Now.Date.AddMonths(4),
                    EndDate = DateTime.Now.Date.AddMonths(5),
                    Description = "Vacation blackout period 2",
                    UserId = new Guid("6786c233-5f89-4e5e-af84-2ff7db03ba86") // User with Admin role
                },
                new IneligiblePeriod
                {
                    Id = 3,
                    StartDate = DateTime.Now.Date.AddMonths(7),
                    EndDate = DateTime.Now.Date.AddMonths(8),
                    Description = "Vacation blackout period 3",
                    UserId = new Guid("6786c233-5f89-4e5e-af84-2ff7db03ba86") // User with Admin role
                },
                new IneligiblePeriod
                {
                    Id = 4,
                    StartDate = DateTime.Now.Date.AddMonths(2).AddDays(10),
                    EndDate = DateTime.Now.Date.AddMonths(2).AddDays(20),
                    Description = "Vacation blackout period 4",
                    UserId = new Guid("6786c233-5f89-4e5e-af84-2ff7db03ba86") // User with Admin role
                },
                new IneligiblePeriod
                {
                    Id = 5,
                    StartDate = DateTime.Now.Date.AddMonths(1),
                    EndDate = DateTime.Now.Date.AddMonths(1).AddDays(5),
                    Description = "Vacation blackout period 5",
                    UserId = new Guid("6786c233-5f89-4e5e-af84-2ff7db03ba86") // User with Admin role
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