using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkHub.Domain.Entities;

namespace WorkHub.Infrastructure.Configurations
{
    public class SpaceMediaConfiguration : IEntityTypeConfiguration<SpaceMedia>
    {
        public void Configure(EntityTypeBuilder<SpaceMedia> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.MediaUrl).IsRequired().HasMaxLength(500);
            builder.Property(x => x.DisplayOrder).IsRequired();
            builder.Property(x => x.IsPrimary).IsRequired().HasDefaultValue(false);
            builder.Property(x=>x.CreatedAtUtc).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.MediaType).IsRequired();
            builder.Property(x => x.SpaceId).IsRequired();
            builder.HasIndex(x=>x.SpaceId).HasFilter("[IsPrimary]=1").IsUnique();
            builder.HasOne(x=>x.Space).WithMany(x=>x.spaceMedias).HasForeignKey(x => x.SpaceId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
