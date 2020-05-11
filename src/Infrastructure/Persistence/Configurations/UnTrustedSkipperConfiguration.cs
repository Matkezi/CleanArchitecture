using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Infrastructure.Persistence.Configurations
{
    public class UnTrustedCharterSkipperConfiguration : IEntityTypeConfiguration<UnTrustedCharterSkipper>
    {
        public void Configure(EntityTypeBuilder<UnTrustedCharterSkipper> builder)
        {
            builder
                .HasKey(t => new { t.SkipperID, t.CharterID });

            builder
                .HasOne(pt => pt.Charter)
                .WithMany(p => p.UnTrustedSkippers)
                .HasForeignKey(pt => pt.CharterID)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(pt => pt.Skipper)
                .WithMany(t => t.UnTrustedCharters)
                .HasForeignKey(pt => pt.SkipperID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
