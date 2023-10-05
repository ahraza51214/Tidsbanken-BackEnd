using System;
using Microsoft.EntityFrameworkCore;
using Tidsbanken_BackEnd.Data.Entities;

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
                .HasForeignKey(c => c.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // VacationRequest one to one VacationRequestStatus : Has
            modelBuilder.Entity<VacationRequest>()
                .Property(vr => vr.Status)
                .HasConversion<String>();
        }
    }
}