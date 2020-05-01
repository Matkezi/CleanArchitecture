using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitecture.Infrastructure.Persistence.Entities;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class SkipperLanguageConfiguration : IEntityTypeConfiguration<SkipperLanguage>
    {
        public void Configure(EntityTypeBuilder<SkipperLanguage> builder)
        {
            builder
               .HasKey(sl => new { sl.SkipperId, sl.LanguageId });

            builder
                .HasOne(bc => bc.Skipper)
                .WithMany(b => b.ListOfLanguages)
                .HasForeignKey(bc => bc.SkipperId);

            builder
                .HasOne(bc => bc.Language)
                .WithMany(c => c.Skippers)
                .HasForeignKey(bc => bc.LanguageId);
        }
    }
}
