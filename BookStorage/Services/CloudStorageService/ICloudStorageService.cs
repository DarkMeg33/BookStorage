namespace BookStorage.Services.CloudStorageService
{
    public interface ICloudStorageService
    {
        Task<Stream> GetFileStreamAsync(string containerName, string filename);
        Task<string> UploadFileAsync(string containerName, string filename, byte[] content);
        Task<bool> DeleteFileAsync(string containerName, string filename);
    }
}