using BookStorage.Services.StorageProviderService;

namespace BookStorage.Services.FileStorageService
{
    public class FileStorageService : IFileStorageService
    {
        private const string BookCoversDirectory = "book-covers";

        private readonly IStorageProviderService _storageProviderService;

        public FileStorageService(IStorageProviderService cloudStorageService)
        {
            _storageProviderService = cloudStorageService;
        }

        #region Book

        public async Task<Stream> GetBookCoverAsync(string filename)
        {
            return await _storageProviderService.GetFileStreamAsync(BookCoversDirectory, filename);
        }

        public async Task<string> SaveBookCoverAsync(string filename, byte[] content)
        {
            return await _storageProviderService.UploadFileAsync(BookCoversDirectory, filename, content);
        }

        public async Task<bool> DeleteBookCoverAsync(string filename)
        {
            return await _storageProviderService.DeleteFileAsync(BookCoversDirectory, filename);
        }

        #endregion
    }
}