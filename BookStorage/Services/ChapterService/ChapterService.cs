using BookStorage.Models.Dto.ChapterDto;
using BookStorage.Models.Dto.FictionBookDto;
using BookStorage.Models.Entities.ChapterEntities;
using BookStorage.Models.ViewModels.ChapterViewModel;
using BookStorage.Repositories.Base;
using BookStorage.Repositories.ChapterRepository;

namespace BookStorage.Services.ChapterService
{
    public class ChapterService : IChapterService
    {
        private readonly IChapterRepository _chapterRepository;

        public ChapterService(IUnitOfWork unitOfWork, IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;

            _chapterRepository.Attach(unitOfWork);
        }

        public async Task<List<ChapterViewModel>> GetChapterViewModelsAsync(int bookId)
        {
            return (await _chapterRepository.GetChaptersMetadataAsync(bookId))
                .Select(x => new ChapterViewModel(x))
                .ToList();
        }

        public async Task<ChapterDto> GetChapterDtoAsync(int chapterId)
        {
            RetrieveChapterEntity entity = await _chapterRepository.GetChapterAsync(chapterId);

            return entity == null ? null : new ChapterDto(entity);
        }
    }
}