using Microsoft.EntityFrameworkCore;
using WorkHub.Application.Interfaces;
using WorkHub.Domain.Entities;
using WorkHub.Infrastructure.Data;
namespace WorkHub.Infrastructure.Repositories
{
    public class SpaceFeatureRepository : ISpaceFeatureRepository
    {
        private readonly AppDbContext _dbContext;
        public SpaceFeatureRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Guid spaceId, Guid featureId)
        {
            var spaceFeature = Domain.Entities.SpaceFeature.Create(spaceId, featureId);
            await _dbContext.SpaceFeatures.AddAsync(spaceFeature);
        }

        public async Task Delete(Guid spaceId, Guid featureId)
        {
            if (spaceId == Guid.Empty)
                throw new ArgumentException("SpaceId cannot be empty.", nameof(spaceId));
            if (featureId == Guid.Empty)
                throw new ArgumentException("FeatureId cannot be empty.", nameof(featureId));

            var existingSpaceFeature = await _dbContext.SpaceFeatures.FirstOrDefaultAsync(sf => sf.SpaceId == spaceId && sf.FeatureId == featureId);

            if(existingSpaceFeature is null)
                throw new KeyNotFoundException("SpaceFeature not found.");
            else
            _dbContext.SpaceFeatures.Remove(existingSpaceFeature);
        }

        public async Task<List<SpaceFeature>> GetBySpaceId(Guid spaceId)
        {
            if (spaceId == Guid.Empty)
                throw new ArgumentException("SpaceId cannot be empty.", nameof(spaceId));

            var spaceFeatures = await  _dbContext.SpaceFeatures.Where(sf => sf.SpaceId == spaceId).ToListAsync();
            return spaceFeatures;
        }

        public async Task<SpaceFeature?> GetBySpaceIdAndFeatureId(Guid spaceId, Guid featureId)
        {
             if(spaceId == Guid.Empty)
                throw new ArgumentException("SpaceId cannot be empty.", nameof(spaceId));
            if (featureId == Guid.Empty)
                throw new ArgumentException("FeatureId cannot be empty.", nameof(featureId));


            var result = await _dbContext.SpaceFeatures.FirstOrDefaultAsync(sf => sf.SpaceId == spaceId && sf.FeatureId == featureId);
            return result; 
        }
    }
}
