using WorkHub.Application.Dtos.spaceMedia;
using WorkHub.Application.Interfaces;
using WorkHub.Domain.Entities;
using WorkHub.Domain.Enums;

namespace WorkHub.Application.Services
{
    public class SpaceMediaService : ISpaceMediaService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IGenericRepository<SpaceMedia, Guid> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpaceService _spaceService;

        public SpaceMediaService(IFileStorageService fileStorageService,
                                IGenericRepository<SpaceMedia, Guid> genericRepository,
                                IUnitOfWork unitOfWork
,
                                ISpaceService spaceService)
        {
            _fileStorageService = fileStorageService;
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
            _spaceService = spaceService;
        }

        public async Task<SpaceMediaResponse> AddAsync(AddSpaceMediaRequest request)
        {
            if(request == null)
                throw new ArgumentNullException(nameof(request), "Request cannot be null.");

            if(request.SpaceId == Guid.Empty)
                throw new ArgumentException("SpaceId cannot be empty.", nameof(request.SpaceId));

            if(request.File == null || request.File.Length == 0)
                throw new ArgumentException("File cannot be null or empty.", nameof(request.File));

            if(await _spaceService.GetSpaceById(request.SpaceId) == null)
                throw new ArgumentException("Space with the given SpaceId does not exist.", nameof(request.SpaceId));

            if(Enum.IsDefined(typeof(MediaType), request.MediaType) == false)
                throw new ArgumentException("Invalid MediaType.", nameof(request.MediaType));

            int displayOrder = await _genericRepository.CountAsync(x => x.SpaceId == request.SpaceId) + 1;

            if(request.IsPrimary)
            {
                var existingPrimary = await _genericRepository.CountAsync(x=>x.SpaceId == request.SpaceId && x.IsPrimary);
                if (existingPrimary > 0)
                {
                    throw new InvalidOperationException("There is already a primary media for this space.");
                }
            }


            using Stream filestream = request.File.OpenReadStream();

            var url = await _fileStorageService.UploadFileAsync(request.SpaceId, request.File.FileName, filestream);

            SpaceMedia spaceMedia = SpaceMedia.Create(request.SpaceId, request.MediaType, url, displayOrder, request.IsPrimary);
            try
            {
                await _genericRepository.Add(spaceMedia);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _fileStorageService.DeleteFileAsync(url);
                throw new Exception("An error occurred while saving the space media.", ex);
            }


            return spaceMedia.ToSpaceMediaResponse(); 
        }

        public async Task DeleteAsync(Guid id)
        {
           if(id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));
           var spaceMedia = await _genericRepository.GetByIdAsync(id);
            if(spaceMedia == null)
                throw new ArgumentException("Space media with the given Id does not exist.", nameof(id));


            await  _genericRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            await _fileStorageService.DeleteFileAsync(spaceMedia.MediaUrl);

        }

        public async Task<SpaceMediaResponse> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));
            var spaceMedia = await _genericRepository.GetByIdAsync(id);
            if (spaceMedia == null)
                throw new KeyNotFoundException("Space media with the given Id does not exist.");

            return spaceMedia.ToSpaceMediaResponse();
        }

        public async Task<List<SpaceMediaResponse>> GetBySpaceIdAsync(Guid spaceId)
        {
            if (spaceId == Guid.Empty)
                throw new ArgumentException("SpaceId cannot be empty.", nameof(spaceId));
            var spaceMediaList = await _genericRepository.GetAllAsync(filter:x=>x.SpaceId == spaceId);
            return spaceMediaList.OrderBy(x=>x.DisplayOrder).Select(x => x.ToSpaceMediaResponse()).ToList();
        }
    }
}

