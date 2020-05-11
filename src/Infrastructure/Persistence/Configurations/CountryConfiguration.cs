using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Infrastructure.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder
                .HasMany(c => c.AppUsers)
                .WithOne(e => e.Country);
        }
    }
}
