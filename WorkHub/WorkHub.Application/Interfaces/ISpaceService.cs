using WorkHub.Application.Dtos.Space;

namespace WorkHub.Application.Interfaces
{
    public interface ISpaceService
    {
        Task<Guid> AddSpaceAsync(AddSpaceRequest request); 

        Task<List<SpaceResponse>> GetAllSpacesAsync(int page=1, int pageSize =10);

        Task<SpaceResponse> GetSpaceById(Guid id); 

        Task SoftDeleteAsync(Guid Id);
        Task UpdateSpaceAsync(Guid id, UpdateSpaceRequest updateSpaceRequest); 
    }
}
