namespace WorkHub.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(Guid SpaceId,string fileName, Stream fileStream);

        Task DeleteFileAsync(string fileUrl);
    }
}
