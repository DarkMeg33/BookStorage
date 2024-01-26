using Azure.Storage;
using BookStorage.Services.CloudStorageService;
using BookStorage.Services.FileStorageService;
using BookStorage.Settings;
using Microsoft.Extensions.Azure;

namespace BookStorage.Extensions
{
    public static class AzureServicesExtensions
    {
        public static void AddAzureBlobStorage(this IServiceCollection services, 
            AppSettings appSettings)
        {
            StorageSharedKeyCredential credential = new StorageSharedKeyCredential(
                appSettings.AzureBlobStorage.StorageName, appSettings.AzureBlobStorage.AccessKey);

            string blobUri = appSettings.AzureBlobStorage.ServiceUri
                .Replace("{storageAccount}", appSettings.AzureBlobStorage.StorageName);

            services.AddAzureClients(cfg =>
            {
                cfg.AddBlobServiceClient(new Uri(blobUri), credential);
            });

            services.AddScoped<ICloudStorageService, AzureBlobStorageService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
        }
    }
}