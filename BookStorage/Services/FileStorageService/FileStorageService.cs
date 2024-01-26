using BookStorage.Services.CloudStorageService;

namespace BookStorage.Services.FileStorageService
{
    public class FileStorageService : IFileStorageService
    {
        private readonly ICloudStorageService _cloudStorageService;

        public FileStorageService(ICloudStorageService cloudStorageService)
        {
            _cloudStorageService = cloudStorageService;
        }
    }
}