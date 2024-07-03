using MeetingManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManagement.Data
{
    public class MeetingContext : DbContext
    {
        public MeetingContext(DbContextOptions<MeetingContext> options) : base(options)
        { 
            
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingType> MeetingTypes { get; set; }
        public DbSet<MeetingItems> MeetingItem { get; set; }
        public DbSet<Status> MeetingItemStatuses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MeetingType>().ToTable("MeetingType");
            modelBuilder.Entity<Meeting>().ToTable("Meeting");
            modelBuilder.Entity<MeetingItems>().ToTable("MeetingItems");
            modelBuilder.Entity<Status>().ToTable("Status");

            // Define relationships and constraints
            modelBuilder.Entity<Meeting>()
                .HasOne(m => m.MeetingType)
                .WithMany()
                .HasForeignKey(m => m.MeetingTypeID);

            modelBuilder.Entity<Status>()
                .HasOne(mis => mis.MeetingItem)
                .WithMany()
                .HasForeignKey(mis => mis.MeetingItemID);

            modelBuilder.Entity<Status>()
                .HasOne(mis => mis.Meeting)
                .WithMany()
                .HasForeignKey(mis => mis.MeetingID);
        }
    }
}
