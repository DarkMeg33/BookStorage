using BookStorage.Helpers.Formatter;
using BookStorage.Helpers.Mapping;
using Newtonsoft.Json;

namespace BookStorage.Settings
{
    public class ConnectionStrings
    {
        [JsonProperty("SqlServer")] public string SqlServer { get; set; }
    }

    public class CookieSettings
    {
        [JsonProperty("CookieName")] public string CookieName { get; set; }
        [JsonProperty("ExpiryAgeInDays")] public int ExpiryAgeInDays { get; set; }
    }

    public class AzureBlobStorage
    {
        [JsonProperty("StorageName")] public string StorageName { get; set; }
        [JsonProperty("AccessKey")] public string AccessKey { get; set; }
        [JsonProperty("ServiceUri")] public string ServiceUri { get; set; }
    }

    public class FileValidationSettings
    {
        [JsonProperty("BookCoverImage")] public FileValidation BookCoverImage { get; set; }
        [JsonProperty("BookFile")] public FileValidation BookFile { get; set; }
    }

    public class FileValidation
    {
        [JsonProperty("MaxAttachmentSize")] public int MaxAttachmentSize { get; set; }
        [JsonProperty("AllowedAttachmentTypes")] public List<string> AllowedAttachmentTypes { get; set; }

        public string MaxAttachmentSizeInMb => StringFormatter.GetFormattedFileSize(MaxAttachmentSize);

        public List<string> AllowedExtensionTypes
        {
            get
            {
                List<string> extensions = new List<string>();
                if (AllowedAttachmentTypes.Any())
                {
                    foreach (string type in AllowedAttachmentTypes)
                    {
                        extensions.AddRange(MimeMapping.GetExtensionsFromMime(type));
                    }    
                }

                return extensions;
            }
        }
    }

    public class AppSettings
    {
        [JsonProperty("ConnectionStrings")] public ConnectionStrings ConnectionStrings { get; set; }
        [JsonProperty("WebsiteName")] public string WebsiteName { get; set; }
        [JsonProperty("TinymceApiKey")] public string TinymceApiKey { get; set; }
        [JsonProperty("CookieSettings")] public CookieSettings CookieSettings { get; set; }
        [JsonProperty("AzureBlobStorage")] public AzureBlobStorage AzureBlobStorage { get; set; }
        [JsonProperty("FileValidationSettings")] public FileValidationSettings FileValidationSettings { get; set; }
    }
}