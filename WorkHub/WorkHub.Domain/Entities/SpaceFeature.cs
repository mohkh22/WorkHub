namespace WorkHub.Domain.Entities
{
    public class SpaceFeature
    {
        public Guid SpaceId { get; private set; }
        public Guid FeatureId { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        public Space Space { get; private set; } = null!; 
        public Feature Feature { get; private set; } = null!;
    }
}
