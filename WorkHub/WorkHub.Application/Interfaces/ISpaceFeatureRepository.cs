using WorkHub.Domain.Entities;

namespace WorkHub.Application.Interfaces
{
    public interface ISpaceFeatureRepository
    {
        Task AddAsync(Guid spaceId, Guid featureId); 
        Task<SpaceFeature?> GetBySpaceIdAndFeatureId(Guid spaceId , Guid featureId);
        Task Delete(Guid spaceId, Guid featureId);
        Task<List<SpaceFeature>> GetBySpaceId(Guid spaceId);

    }
}
