using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkHub.Domain.Entities;
using WorkHub.Domain.Enums;

namespace WorkHub.Infrastructure.Configurations
{
    public class SpaceConfiguration : IEntityTypeConfiguration<Space>
    {
        public void Configure(EntityTypeBuilder<Space> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SpaceNumber).IsRequired().HasMaxLength(20); 
            builder.Property(x=>x.HourlyPrice).HasPrecision(18, 2);
            builder.Property(x=>x.DailyPrice).HasPrecision(18, 2);
            builder.HasIndex(x => x.SpaceNumber).IsUnique().HasFilter("[IsDeleted] = 0");
            builder.Property(x=>x.Status).HasDefaultValue(SpaceStatus.Available);
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("ck_space_min_capacity", "[MinCapacity] > 0");
                t.HasCheckConstraint("ck_space_max_capacity", "[MaxCapacity] >= [MinCapacity]"); 
                t.HasCheckConstraint("ck_space_hourly_price", "[HourlyPrice] > 0");
                t.HasCheckConstraint("ck_space_daily_price", "[DailyPrice] > 0");
            });
            builder.Property(x => x.CreatedAtUtc).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

        }
    }
}
