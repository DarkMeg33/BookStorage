using BookStorage.Models.Dto.ChapterDto;
using BookStorage.Models.Dto.FictionBookDto;
using BookStorage.Models.ViewModels.ChapterViewModel;

namespace BookStorage.Services.ChapterService
{
    public interface IChapterService
    {
        Task<List<ChapterViewModel>> GetChapterViewModelsAsync(int bookId);
        Task<ChapterDto> GetChapterDtoAsync(int chapterId);
    }
}