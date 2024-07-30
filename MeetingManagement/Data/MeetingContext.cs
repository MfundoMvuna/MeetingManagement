using MeetingManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagement.Data
{
    public class MeetingContext : DbContext
    {
        public MeetingContext(DbContextOptions<MeetingContext> options) : base(options)
        {
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingType> MeetingTypes { get; set; }
        public DbSet<MeetingItems> MeetingItems { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table configurations
            modelBuilder.Entity<MeetingType>().ToTable("MeetingType");
            modelBuilder.Entity<Meeting>().ToTable("Meeting");
            modelBuilder.Entity<MeetingItems>().ToTable("MeetingItems");
            modelBuilder.Entity<Status>().ToTable("Status");

            // Relationships configurations
            modelBuilder.Entity<MeetingItems>()
                .HasMany(mi => mi.Statuses)
                .WithOne(s => s.MeetingItem)
                .HasForeignKey(s => s.MeetingItemID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Meeting>()
                .HasMany(m => m.MeetingItems)
                .WithOne(mi => mi.Meeting)
                .HasForeignKey(mi => mi.MeetingID);

            modelBuilder.Entity<Status>()
                .HasOne(m => m.MeetingItem)
                .WithMany(mi => mi.Statuses)
                .HasForeignKey(ms => ms.MeetingItemID);


            // Ensure nullable fields are properly handled
            modelBuilder.Entity<MeetingItems>()
                .Property(mi => mi.Description)
                .IsRequired();

            modelBuilder.Entity<MeetingItems>()
                .Property(mi => mi.PersonResponsible)
                .IsRequired();

            modelBuilder.Entity<Meeting>()
                .Property(m => m.Title)
                .IsRequired();

            modelBuilder.Entity<Meeting>()
                .Property(m => m.MeetingNumber)
                .IsRequired();
        }
    }
}