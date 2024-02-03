namespace BookStorage.Services.FileStorageService
{
    public interface IFileStorageService
    {
        #region Book

        Task<Stream> GetBookCoverAsync(string filename);
        Task<string> SaveBookCoverAsync(string filename, byte[] content);
        Task<bool> DeleteBookCoverAsync(string filename);

        #endregion
    }
}