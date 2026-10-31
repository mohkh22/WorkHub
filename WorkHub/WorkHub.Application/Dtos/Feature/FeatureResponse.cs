 
namespace WorkHub.Application.Dtos.Feature
{
    public class FeatureResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }

    public static class FeatureExtension
    {
        public static FeatureResponse ToFeatureResponse(this WorkHub.Domain.Entities.Feature feature)
        {
            if (feature is null)
                throw new ArgumentNullException(nameof(feature),"can not convert from null to Feature Response");

            return new FeatureResponse
            {
                Id = feature.Id,
                Name = feature.Name,
                Description = feature.Description
            }; 
        }

        public static UpdateFeatureRequest ToUpdateFeatureRequest(this FeatureResponse featureResponse)
        {
            if (featureResponse is null)
                throw new ArgumentNullException(nameof(featureResponse), "can not convert from null to Update Feature Request");

            return new UpdateFeatureRequest
            {
                Name = featureResponse.Name,
                Description = featureResponse.Description
            };
        }
    }
}
