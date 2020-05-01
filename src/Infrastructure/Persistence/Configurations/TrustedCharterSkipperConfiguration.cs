using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitecture.Infrastructure.Persistence.Entities;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class TrustedCharterSkipperConfiguration : IEntityTypeConfiguration<TrustedCharterSkipper>
    {
        public void Configure(EntityTypeBuilder<TrustedCharterSkipper> builder)
        {
            builder
                .HasKey(t => new { t.SkipperID, t.CharterID });

            builder
                .HasOne(pt => pt.Charter)
                .WithMany(p => p.TrustedSkippers)
                .HasForeignKey(pt => pt.CharterID)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(pt => pt.Skipper)
                .WithMany(t => t.TrustedCharters)
                .HasForeignKey(pt => pt.SkipperID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
