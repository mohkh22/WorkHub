using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkHub.Domain.Entities;

namespace WorkHub.Infrastructure.Configurations
{
    public class SpaceFeatureConfiguration : IEntityTypeConfiguration<SpaceFeature>
    {
        public void Configure(EntityTypeBuilder<SpaceFeature> builder)
        {
            builder.HasKey(x => new
            {
                x.SpaceId,
                x.FeatureId
            });

            builder.HasOne<Space>()
                .WithMany(x=>x.SpaceFeatures)
                .HasForeignKey(x=>x.SpaceId)
                .OnDelete(DeleteBehavior.NoAction); 
            builder.HasOne<Feature>()
                .WithMany(x => x.SpaceFeatures)
                .HasForeignKey(x=>x.FeatureId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(x => x.CreatedAtUtc).HasDefaultValueSql("GETUTCDATE()"); 
        }
    }
}
