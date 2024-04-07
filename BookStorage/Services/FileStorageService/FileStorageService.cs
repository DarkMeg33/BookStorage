using BookStorage.Services.StorageProviderService;

namespace BookStorage.Services.FileStorageService
{
    public class FileStorageService : IFileStorageService
    {
        private const string BookCoversDirectory = "book-covers";
        private const string AvatarsDirectory = "avatars";

        private readonly IStorageProviderService _storageProviderService;

        public FileStorageService(IStorageProviderService cloudStorageService)
        {
            _storageProviderService = cloudStorageService;
        }

        #region Book

        public async Task<Stream> GetBookCoverAsync(string storageReference)
        {
            return await _storageProviderService.GetFileStreamAsync(BookCoversDirectory, storageReference);
        }

        public async Task<string> SaveBookCoverAsync(string filename, byte[] content)
        {
            return await _storageProviderService.UploadFileAsync(BookCoversDirectory, GenerateReference(filename), content);
        }

        public async Task<bool> DeleteBookCoverAsync(string storageReference)
        {
            return await _storageProviderService.DeleteFileAsync(BookCoversDirectory, storageReference);
        }

        #endregion

        #region Avatar

        public async Task<Stream> GetAvatarAsync(string storageReference)
        {
            return await _storageProviderService.GetFileStreamAsync(AvatarsDirectory, storageReference);
        }

        public async Task<string> SaveAvatarAsync(string filename, byte[] content)
        {
            return await _storageProviderService.UploadFileAsync(AvatarsDirectory, GenerateReference(filename), content);
        }

        public async Task<bool> DeleteAvatarAsync(string storageReference)
        {
            return await _storageProviderService.DeleteFileAsync(AvatarsDirectory, storageReference);
        }

        #endregion

        #region Private

        private string GenerateReference(string originalName)
        {
            return $"{Guid.NewGuid().ToString()}{Path.GetExtension(originalName)}";
        }

        #endregion
    }
}