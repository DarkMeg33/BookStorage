using BookStorage.Models.Dto.FictionBookDto;

namespace BookStorage.Services.FictionBookReaderService
{
    public interface IFictionBookReaderService
    {
        Task<FictionBookDto> ReadDocumentAsync(IFormFile file);
    }
}