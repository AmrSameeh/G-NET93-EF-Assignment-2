using EventHub.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.Configurations
{
    public class OrganizerProfileConfiguration
        : IEntityTypeConfiguration<OrganizerProfile>
    {
        public void Configure(
            EntityTypeBuilder<OrganizerProfile> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Bio)
                   .HasMaxLength(500);

            builder.Property(p => p.Website)
                   .HasMaxLength(250);

            builder.Property(p => p.Logo)
                   .HasMaxLength(500);

            // One-to-One
            builder.HasOne(p => p.Organizer)
                   .WithOne(o => o.Profile)
                   .HasForeignKey<OrganizerProfile>(
                       p => p.OrganizerId)
                   .IsRequired();
        }
    }
}