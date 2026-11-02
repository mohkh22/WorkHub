using WorkHub.Domain.Enums;

namespace WorkHub.Application.Dtos.Space
{
    public class SpaceResponse
    {
        public Guid Id { get;  set; }
        public string SpaceNumber { get; set; } = null!; 
        public string? Description { get;  set; }

        public SpaceStatus Status { get;  set; }

        public SpaceType Type { get;  set; }

        public int MinCapacity { get;  set; }
        public int MaxCapacity { get;  set; }
        public decimal HourlyPrice { get;  set; }
        public decimal DailyPrice { get;  set; }

        public List<Guid> FeatureIds { get; set; } = new();
        public List<string> FeatureNames {get; set; } = new();

    }

    public static class SpaceResponseExtensions
    {
        public static SpaceResponse ToSpaceResponse(this Domain.Entities.Space space)
        {
            if (space == null)
            {
                throw new ArgumentNullException(nameof(space));
            }
            return new SpaceResponse
            {
                Id = space.Id,
                SpaceNumber = space.SpaceNumber,
                Description = space.Description,
                Status = space.Status,
                Type = space.Type,
                MinCapacity = space.MinCapacity,
                MaxCapacity = space.MaxCapacity,
                HourlyPrice = space.HourlyPrice,
                DailyPrice = space.DailyPrice,
                FeatureIds = space.SpaceFeatures
                            .Select(sf => sf.FeatureId)
                            .ToList(),
                FeatureNames = space.SpaceFeatures
                            .Select(sf => sf.Feature?.Name)
                            .ToList()


            };
        }

        public static UpdateSpaceRequest ToUpdateSpaceRequest(this SpaceResponse spaceResponse)
        {
            if(spaceResponse is null)
                throw new ArgumentNullException(nameof(spaceResponse));

            return new UpdateSpaceRequest
            {
                SpaceNumber = spaceResponse.SpaceNumber,
                Description= spaceResponse.Description,
                Type = spaceResponse.Type, 
                MinCapacity = spaceResponse.MinCapacity,
                MaxCapacity=spaceResponse.MaxCapacity,
                HourlyPrice=spaceResponse.HourlyPrice,
                DailyPrice=spaceResponse.DailyPrice,
                FeatureIds = spaceResponse.FeatureIds

            }; 
        }
    }

}
