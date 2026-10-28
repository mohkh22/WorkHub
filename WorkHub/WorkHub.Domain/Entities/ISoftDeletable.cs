namespace WorkHub.Domain.Entities
{
    public interface ISoftDeletable
    {
        public bool IsDeleted { get;  }
        void SoftDelete(); 
    }
}
