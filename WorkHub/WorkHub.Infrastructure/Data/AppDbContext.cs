using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using WorkHub.Domain.Entities;

namespace WorkHub.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Space> Spaces { get; set; } 
        public DbSet<Feature> Features { get; set; } 
        public DbSet<SpaceFeature> SpaceFeatures { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
           foreach(var item in modelBuilder.Model.GetEntityTypes())
            {
                if(typeof(ISoftDeletable).IsAssignableFrom(item.ClrType))
                {
                    ParameterExpression parameterExpression = Expression.Parameter(item.ClrType,"x");

                    var property = Expression.Property(parameterExpression, "IsDeleted");
                    var ExpressionNot = Expression.Not(property);
                    var lamada = Expression.Lambda(ExpressionNot, parameterExpression); 
                    modelBuilder.Entity(item.ClrType).HasQueryFilter(lamada); 
                }
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
