using WorkHub.Application.Dtos.Space;
using WorkHub.Application.Interfaces;
using WorkHub.Domain.Entities;

namespace WorkHub.Application.Services
{
    public class SpaceService : ISpaceService
    {
        private readonly IGenericRepository<Space, Guid> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SpaceService(IGenericRepository<Space,Guid> genericRepository , IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> AddSpaceAsync(AddSpaceRequest request)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Space newSpace = Space.Create(request.SpaceNumber,request.Description,request.Type, request.MinCapacity, request.MaxCapacity, request.HourlyPrice, request.DailyPrice);

           await  _genericRepository.Add(newSpace);

            await _unitOfWork.SaveChangesAsync();

            return newSpace.Id; 
        }

        public async Task<List<SpaceResponse>> GetAllSpacesAsync(int page = 1, int pageSize = 10)
        {
             var spaces = await _genericRepository.GetAllAsync(page,pageSize);
            List<SpaceResponse> spaceResponses = spaces.Select(x => x.ToSpaceResponse()).ToList(); 

            return spaceResponses; 
        }

        public async Task<SpaceResponse> GetSpaceById(Guid id)
        {
            var space = await _genericRepository.GetByIdAsync(id);

            if (space is null)
                throw new KeyNotFoundException("Id is not found");
            else
                return space.ToSpaceResponse(); 
        }

        public async Task SoftDeleteAsync(Guid Id)
        {
            Space? space = await _genericRepository.GetByIdAsync(Id);
            if (space is null)
                throw new KeyNotFoundException("Id Is not found");

            space.SoftDelete();
            await _unitOfWork.SaveChangesAsync(); 
        }
    }
}
