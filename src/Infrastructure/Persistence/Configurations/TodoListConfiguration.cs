using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Infrastructure.Persistence.Configurations
{
    public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
    {
        public void Configure(EntityTypeBuilder<TodoList> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
