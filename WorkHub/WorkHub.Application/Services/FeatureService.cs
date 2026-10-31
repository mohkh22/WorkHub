using WorkHub.Application.Dtos.Feature;
using WorkHub.Application.Interfaces;
using WorkHub.Domain.Entities;

namespace WorkHub.Application.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly IGenericRepository<Feature, Guid> _genericRepository; 
        private readonly IUnitOfWork _unitOfWork;
        public FeatureService(IGenericRepository<Feature, Guid> genericRepository, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> AddFeatureAsync(AddFeatureRequest addFeatureRequest)
        {
            if (addFeatureRequest is null)
                throw new ArgumentNullException(nameof(addFeatureRequest), "Cannot add null");

            var feature = Feature.Create(addFeatureRequest.Name, addFeatureRequest.Description);


            await  _genericRepository.Add(feature);
            await _unitOfWork.SaveChangesAsync(); 
            return feature.Id ; 

        }

        public async Task<List<FeatureResponse>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            var features = await _genericRepository.GetAllAsync(page, pageSize);

            var result = features.Select(x => x.ToFeatureResponse()).ToList() ;

            return result; 
        }

        public async Task<FeatureResponse?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("id is not correct");

            var feature = await _genericRepository.GetByIdAsync(id);
            if (feature is null)
                throw new KeyNotFoundException($"Feature with {id} not found");

            return feature.ToFeatureResponse(); 
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("id is not correct");

            var feature = await _genericRepository.GetByIdAsync(id);
            if (feature is null)
                throw new KeyNotFoundException($"Feature with {id} not found");

            feature.SoftDelete();
            await _unitOfWork.SaveChangesAsync(); 
        }

        public async Task UpdateFeatureAsync(Guid id, UpdateFeatureRequest updateFeatureRequest)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("id is not correct");

            if (updateFeatureRequest is null)
                throw new ArgumentNullException(nameof(updateFeatureRequest), "can not null"); 

            var feature = await _genericRepository.GetByIdAsync(id);
            if (feature is null)
                throw new KeyNotFoundException($"Feature with {id} not found");

            feature.Update(updateFeatureRequest.Name, updateFeatureRequest.Description);
            await _unitOfWork.SaveChangesAsync(); 
        }
    }
}
