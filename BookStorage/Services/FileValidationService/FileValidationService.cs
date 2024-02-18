using BookStorage.Models.Enums;
using BookStorage.Settings;
using Microsoft.AspNetCore.StaticFiles;

namespace BookStorage.Services.FileValidationService
{
    public class FileValidationService : IFileValidationService
    {
        private readonly AppSettings _appSettings;

        public FileValidationService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public bool IsBookCoverValid(IFormFile file, out string errorMessage)
        {
            return TryValidateFile(new ValidationFile(file),
                _appSettings.FileValidationSettings.BookCoverImage.MaxAttachmentSize,
                _appSettings.FileValidationSettings.BookCoverImage.AllowedAttachmentTypes,
                out errorMessage, out var _);
        }

        public bool IsBookFileValid(IFormFile file, out string errorMessage)
        {
            return TryValidateFile(new ValidationFile(file),
                _appSettings.FileValidationSettings.BookFile.MaxAttachmentSize,
                _appSettings.FileValidationSettings.BookFile.AllowedAttachmentTypes,
                out errorMessage, out var _);
        }

        #region Private

        private bool IsValidFile(ValidationFile file, int? maxSize, List<string> acceptableFormats,
            out bool invalidSize, out bool invalidFormat)
        {
            invalidFormat = false;
            invalidSize = false;

            if (maxSize != null)
            {
                if (file.Length > maxSize)
                {
                    invalidSize = true;

                    return false;
                }
            }

            if (acceptableFormats != null && acceptableFormats.Any())
            {
                if (!(new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out string mimeType)))
                {
                    invalidFormat = true;

                    return false;
                }

                if (!acceptableFormats.Any(x => x.Equals(mimeType, StringComparison.InvariantCultureIgnoreCase)))
                {
                    invalidFormat = true;

                    return false;
                }
            }

            return true;
        }

        private bool TryValidateFile(ValidationFile file, int? maxSize, List<string> acceptableFormats,
            out string errorMessage, out FileValidationError? fileValidationError)
        {
            errorMessage = null;
            fileValidationError = null;

            if (!IsValidFile(file, maxSize, acceptableFormats, out bool invalidSize, out bool invalidFormat))
            {
                if (invalidSize)
                {
                    errorMessage = $"File has bigger size than expected. Max size is {maxSize / 1024}KB";
                    fileValidationError = FileValidationError.InvalidSize;

                    return false;
                }

                if (invalidFormat)
                {
                    errorMessage = $"{Path.GetExtension(file.FileName)} is not an allowed extension.";
                    fileValidationError = FileValidationError.InvalidFormat;

                    return false;
                }
            }

            return true;
        }

        private class ValidationFile
        {
            public string FileName { get; set; }
            public long Length { get; set; }

            public ValidationFile(IFormFile file)
            {
                FileName = file.FileName;
                Length = file.Length;
            }
        }

        #endregion
    }
}