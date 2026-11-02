namespace WorkHub.Domain.Entities
{
    public class SpaceFeature
    {
        public Guid SpaceId { get; private set; }
        public Guid FeatureId { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public Space Space { get; private set; } = null!; 
        public Feature Feature { get; private set; } = null!;

        private SpaceFeature(Guid SpaceId, Guid FeatureId)
        {
            this.SpaceId = SpaceId;
            this.FeatureId = FeatureId;
            CreatedAtUtc = DateTime.UtcNow;
        }

        public static SpaceFeature Create(Guid SpaceId, Guid FeatureId)
        {
            if(SpaceId == Guid.Empty)
                throw new ArgumentException("SpaceId cannot be empty.", nameof(SpaceId));
            if(FeatureId == Guid.Empty)
                throw new ArgumentException("FeatureId cannot be empty.", nameof(FeatureId));

            return new SpaceFeature(SpaceId, FeatureId);
        }


    }
}
