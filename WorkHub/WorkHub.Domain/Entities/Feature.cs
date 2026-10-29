namespace WorkHub.Domain.Entities
{
    public class Feature:ISoftDeletable
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!; 
        public string? Description { get; private set; }
        public bool IsDeleted { get; private set; }
        public ICollection<SpaceFeature> SpaceFeatures { get; private set; } = new List<SpaceFeature>(); 

        private Feature(string name,string? description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;           
        }


        public static Feature Create(string name, string? description)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"{nameof(name)} can not Empty or null");

            return new Feature(name, description); 
        }

        public void Update(string name , string? description)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"{nameof(name)} can not Empty or null");

            Name = name;
            Description = description; 
        }
       
        
        public void SoftDelete()
        {
            IsDeleted = true; 
        }

        public void Restore()
        {
            IsDeleted = false; 
        }

    }
}
