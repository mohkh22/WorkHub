using WorkHub.Domain.Enums;

namespace WorkHub.Application.Dtos.spaceMedia
{
    public class SpaceMediaResponse
    {
        public Guid Id { get;  set; }
        public Guid SpaceId { get;  set; }
        public MediaType MediaType { get;  set; }
        public string MediaUrl { get;  set; } = null!;

        public int DisplayOrder { get;  set; }

        public bool IsPrimary { get;  set; }
    }

    public static class SpaceMediaResponseExtensions
    {
        public static SpaceMediaResponse ToSpaceMediaResponse(this Domain.Entities.SpaceMedia spaceMedia)
        {
            return new SpaceMediaResponse
            {
                Id = spaceMedia.Id,
                SpaceId = spaceMedia.SpaceId,
                MediaType = spaceMedia.MediaType,
                MediaUrl = spaceMedia.MediaUrl,
                DisplayOrder = spaceMedia.DisplayOrder,
                IsPrimary = spaceMedia.IsPrimary
            };
        }
    }
}
