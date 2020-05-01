using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitecture.Infrastructure.Persistence.Entities;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class SkipperSkillConfiguration : IEntityTypeConfiguration<SkipperSkill>
    {
        public void Configure(EntityTypeBuilder<SkipperSkill> builder)
        {
            builder
                .HasKey(bc => new { bc.SkipperId, bc.SkillId });

            builder
                .HasOne(bc => bc.Skipper)
                .WithMany(b => b.ListOfSkills)
                .HasForeignKey(bc => bc.SkipperId);

            builder
                .HasOne(bc => bc.Skill)
                .WithMany(c => c.Skippers)
                .HasForeignKey(bc => bc.SkillId);
        }
    }
}
