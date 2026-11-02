using EventHub.Entities;
using EventHub.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Data
{
    public class EventHubDbContext : DbContext
    {
        public EventHubDbContext()
        {
        }

        public EventHubDbContext(
            DbContextOptions<EventHubDbContext> options)
            : base(options)
        {
        }

        public DbSet<Organizer> Organizers { get; set; }

        public DbSet<OrganizerProfile> OrganizerProfiles { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Attendee> Attendees { get; set; }

        public DbSet<Badge> Badges { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=.;Database=EventHubDB;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(
                new OrganizerProfileConfiguration());

            modelBuilder.ApplyConfiguration(
                new BadgeConfiguration());

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Description)
                      .IsRequired()
                      .HasMaxLength(2000);

                entity.Property(e => e.StartDate)
                      .IsRequired();

                entity.Property(e => e.EndDate)
                      .IsRequired(false);

                entity.Property(e => e.MaxAttendees)
                      .IsRequired();


                entity.HasOne(e => e.Organizer)
                      .WithMany(o => o.Events)
                      .HasForeignKey(e => e.OrganizerId)
                      .IsRequired();
            });
        }
    }
}