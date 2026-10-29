using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkHub.Domain.Entities;

namespace WorkHub.Infrastructure.Configurations
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}
