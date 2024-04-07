namespace BookStorage.Services.FileValidationService
{
    public interface IFileValidationService
    {
        bool IsBookCoverValid(IFormFile file, out string errorMessage);
        bool IsBookFileValid(IFormFile file, out string errorMessage);
        bool IsAvatarValid(IFormFile file, out string errorMessage);
    }
}