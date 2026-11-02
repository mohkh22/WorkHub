using WorkHub.Domain.Enums;

namespace WorkHub.Domain.Entities
{
    public class SpaceMedia
    {
        public Guid Id { get; private set; }
        public Guid SpaceId { get; private set; }
        public MediaType MediaType { get; private set; }
        public string MediaUrl { get; private set; } = null!;

        public int DisplayOrder { get; private set; }

        public bool IsPrimary { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }

        private SpaceMedia(Guid spaceId , MediaType mediaType, string mediaUrl,int displayOrder,bool isPrimary=false)
        {
            Id = Guid.NewGuid();
            SpaceId = spaceId;
            MediaType = mediaType;
            MediaUrl = mediaUrl;
            DisplayOrder = displayOrder;
                IsPrimary = isPrimary;
            CreatedAtUtc = DateTime.UtcNow;

        }

        public static SpaceMedia Create(Guid spaceId, MediaType mediaType, string mediaUrl, int displayOrder, bool isPrimary = false)
        {
            if(spaceId == Guid.Empty)
                throw new ArgumentException("SpaceId cannot be empty.", nameof(spaceId));

            if(Enum.IsDefined(typeof(MediaType), mediaType) == false)
                throw new ArgumentException("Invalid MediaType.", nameof(mediaType));

            if(String.IsNullOrWhiteSpace(mediaUrl))
                throw new ArgumentException("MediaUrl cannot be null or empty.", nameof(mediaUrl));
            if (displayOrder <= 0)
                throw new ArgumentException("DisplayOrder cannot be zero or negative.", nameof(displayOrder));

            return new SpaceMedia(spaceId, mediaType, mediaUrl, displayOrder, isPrimary);
        }
    }
}
