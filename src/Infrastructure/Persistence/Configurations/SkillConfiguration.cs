using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;

namespace SkipperAgency.Infrastructure.Persistence.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder
                .HasData(
                new Skill
                {
                    Id = SkillsEnum.Cook,
                    Name = "Cook",
                    Icon = "fas fa-utensils"
                },
                new Skill
                {
                    Id = SkillsEnum.Freedive,
                    Name = "Diving",
                    Icon = "fas fa-swimmer"
                },
                new Skill
                {
                    Id = SkillsEnum.LocalExpert,
                    Name = "Local expert",
                    Icon = "fas fa-search-location"
                },
                new Skill
                {
                    Id = SkillsEnum.VideoCreator,
                    Name = "Video",
                    Icon = "fas fa-video"
                },
                new Skill
                {
                    Id = SkillsEnum.Photographer,
                    Name = "Photo",
                    Icon = "fas fa-camera"
                }
            );
        }
    }
}
