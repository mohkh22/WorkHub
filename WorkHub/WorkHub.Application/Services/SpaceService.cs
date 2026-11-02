using WorkHub.Application.Dtos.Space;
using WorkHub.Application.Interfaces;
using WorkHub.Domain.Entities;

namespace WorkHub.Application.Services
{
    public class SpaceService : ISpaceService
    {
        private readonly IGenericRepository<Space, Guid> _genericRepository;
        private readonly ISpaceFeatureRepository _spaceFeatureRepository;
        private readonly IFeatureService _featureService;
        private readonly IUnitOfWork _unitOfWork;
        public SpaceService(IGenericRepository<Space, Guid> genericRepository, IUnitOfWork unitOfWork, ISpaceFeatureRepository spaceFeatureRepository, IFeatureService featureService)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
            _spaceFeatureRepository = spaceFeatureRepository;
            _featureService = featureService;
        }
        public async Task<Guid> AddSpaceAsync(AddSpaceRequest request)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            foreach (var featureId in request.FeatureIds.Distinct())
            {
                if(featureId == Guid.Empty)
                    throw new ArgumentException("FeatureId cannot be empty.", nameof(featureId));
                 await _featureService.GetByIdAsync(featureId);
            }

            Space newSpace = Space.Create(request.SpaceNumber,request.Description,request.Type, request.MinCapacity, request.MaxCapacity, request.HourlyPrice, request.DailyPrice);

           await  _genericRepository.Add(newSpace);

            foreach (var featureId in request.FeatureIds.Distinct())
            {
                await _spaceFeatureRepository.AddAsync(newSpace.Id, featureId);
            }

            await _unitOfWork.SaveChangesAsync();

            return newSpace.Id; 
        }

        public async Task<List<SpaceResponse>> GetAllSpacesAsync(int page = 1, int pageSize = 10)
        {
             var spaces = await _genericRepository.GetAllAsync(page,pageSize);
            List<SpaceResponse> spaceResponses = spaces.Select(x => x.ToSpaceResponse()).ToList(); 

            return spaceResponses; 
        }

        public async Task<SpaceResponse> GetSpaceById(Guid id,string includeProperties="")
        {
            var space = await _genericRepository.GetByIdAsync(id, includeProperties);

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

        public async Task UpdateSpaceAsync(Guid id, UpdateSpaceRequest updateSpaceRequest)
        {
            if (updateSpaceRequest is null)
                throw new ArgumentNullException(nameof(updateSpaceRequest));

            if (id == Guid.Empty)
                throw new ArgumentException("Enter correct Id");
            else
            {
                var space = await _genericRepository.GetByIdAsync(id,nameof(Space.SpaceFeatures));
                if (space is null)
                    throw new KeyNotFoundException("Space Not Found");

                foreach (var featureId in updateSpaceRequest.FeatureIds.Distinct())
                {
                    if (featureId == Guid.Empty)
                        throw new ArgumentException("FeatureId cannot be empty.", nameof(featureId));
                    await _featureService.GetByIdAsync(featureId);
                }

                space.UpdateDetails(updateSpaceRequest.SpaceNumber, updateSpaceRequest.Description);
                space.UpdateCapacity(updateSpaceRequest.MinCapacity, updateSpaceRequest.MaxCapacity);
                space.UpdatePricing(updateSpaceRequest.HourlyPrice, updateSpaceRequest.DailyPrice);
                space.UpdateType(updateSpaceRequest.Type);

                var existingSpaceFeatures = await _spaceFeatureRepository.GetBySpaceId(id);

                foreach (var item in existingSpaceFeatures)
                {
                    if(!updateSpaceRequest.FeatureIds.Contains(item.FeatureId))
                    {
                        await _spaceFeatureRepository.Delete(id, item.FeatureId);
                    }

                }

                foreach (var featureId in updateSpaceRequest.FeatureIds.Distinct())
                {
                    if(existingSpaceFeatures.Any(sf => sf.FeatureId == featureId) == false)
                    {
                        await _spaceFeatureRepository.AddAsync(id, featureId);
                    }
                }
                await _unitOfWork.SaveChangesAsync(); 
            }
        }
    }
}
