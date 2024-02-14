using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BookStorage.Services.StorageProviderService
{
    public class AzureBlobStorageService : IStorageProviderService
    {
        private readonly BlobServiceClient _serviceClient;

        public AzureBlobStorageService(BlobServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task<Stream> GetFileStreamAsync(string containerName, string filename)
        {
            try
            {
                BlobContainerClient containerClient = _serviceClient.GetBlobContainerClient(containerName);
                BlobClient blobClient = containerClient.GetBlobClient(filename);

                return await blobClient.OpenReadAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<string> UploadFileAsync(string containerName, string filename, byte[] content)
        {
            try
            {
                BlobContainerClient containerClient = _serviceClient.GetBlobContainerClient(containerName);
                await containerClient.CreateIfNotExistsAsync();
                BlobClient blobClient = containerClient.GetBlobClient(filename);

                await using Stream stream = new MemoryStream(content);
                await blobClient.UploadAsync(stream);

                return filename;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> DeleteFileAsync(string containerName, string filename)
        {
            try
            {
                BlobContainerClient containerClient = _serviceClient.GetBlobContainerClient(containerName);
                BlobClient blobClient = containerClient.GetBlobClient(filename);

                return (await blobClient.DeleteIfExistsAsync()).Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}