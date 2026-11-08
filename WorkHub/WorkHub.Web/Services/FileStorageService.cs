using WorkHub.Application.Interfaces;

namespace WorkHub.Web.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<string> UploadFileAsync(Guid SpaceId, string fileName, Stream fileStream)
        {
            if (SpaceId == Guid.Empty)
                throw new ArgumentException("SpaceId cannot be empty.", nameof(SpaceId));
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("FileName cannot be null or empty.", nameof(fileName));
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("FileStream cannot be null or empty.", nameof(fileStream));

            if (_webHostEnvironment == null || string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
                throw new InvalidOperationException("WebRootPath is not configured properly.");


            if (!IsValidExtentionAndSize(fileName, fileStream))
                throw new InvalidOperationException("Invalid file extension or size. Only .jpg, .jpeg, .png, .webp, and .mp4 are allowed.");

            Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "spaces", SpaceId.ToString()));

            var newfileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName).ToLowerInvariant()}";

            string relativePath = $"/uploads/spaces/{SpaceId.ToString()}/{newfileName}";

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath,
                relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            using var destinationFileStream = new FileStream(filePath, FileMode.CreateNew);
            await fileStream.CopyToAsync(destinationFileStream);

            return relativePath;
        }
        public Task DeleteFileAsync(string fileUrl)
        {
            if (string.IsNullOrWhiteSpace(fileUrl))
                throw new ArgumentException("FileUrl cannot be null or empty.", nameof(fileUrl));

            if (_webHostEnvironment == null ||
                string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
                throw new InvalidOperationException("WebRootPath is not configured properly.");


            if (!fileUrl.StartsWith("/uploads/spaces/", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Invalid file path.");

            if (fileUrl.Contains('\\') || fileUrl.Contains('?') ||fileUrl.Contains('#'))
            {
                throw new InvalidOperationException("Invalid file path.");
            }
            var relativePath = fileUrl.TrimStart('/') .Replace('/', Path.DirectorySeparatorChar);

            var physicalFilePath = Path.GetFullPath(
                Path.Combine(_webHostEnvironment.WebRootPath, relativePath));


            var allowedDirectory = Path.GetFullPath(
                Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "spaces"))
                + Path.DirectorySeparatorChar;


            if (!physicalFilePath.StartsWith(
                allowedDirectory, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Invalid file path.");
            }

            if (File.Exists(physicalFilePath))
                File.Delete(physicalFilePath);

            return Task.CompletedTask;
        
        }

        private bool IsValidExtentionAndSize(string fileName,Stream fileStream)
        {
            var extension = Path.GetExtension(fileName)?.ToLowerInvariant();
            const long maxImageSize = 5 * 1024 * 1024; // 5 MB
            const long maxVideoSize = 50 * 1024 * 1024; // 50 MB
            if (string.IsNullOrEmpty(extension))
                return false;
            if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".webp")
            {
                    return fileStream.Length <= maxImageSize;
            }
            else if(extension == ".mp4")
            {
                    return fileStream.Length <= maxVideoSize;
            }

            return false; 

        }
      
       
    }
}
