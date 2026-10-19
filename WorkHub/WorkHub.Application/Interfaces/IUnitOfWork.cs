namespace WorkHub.Application.Interfaces
{
    public interface IUnitOfWork
    {
       public  Task<int> SaveChangesAsync(); 
    }
}
