namespace BookStorage.Services.FileValidationService
{
    public interface IFileValidationService
    {
        bool IsBookCoverValid(IFormFile file, out string errorMessage);
    }
}