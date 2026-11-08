using WorkHub.Application.Dtos.spaceMedia;

namespace WorkHub.Application.Interfaces
{
    public interface ISpaceMediaService
    {
        Task<SpaceMediaResponse> AddAsync(AddSpaceMediaRequest request);
        Task DeleteAsync(Guid id); 
        Task<SpaceMediaResponse> GetByIdAsync(Guid id);

         Task<List<SpaceMediaResponse>> GetBySpaceIdAsync(Guid spaceId);
    }
}
