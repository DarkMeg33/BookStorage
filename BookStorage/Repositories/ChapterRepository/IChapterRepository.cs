using BookStorage.Models.Entities.ChapterEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.ChapterRepository
{
    public interface IChapterRepository : IRepository
    {
        Task<List<RetrieveChapterEntity>> GetChaptersAsync(int bookId);
        Task<RetrieveChapterEntity> GetChapterAsync(int chapterId);
        Task<ChapterMetadataEntity> GetChapterMetadataAsync(int chapterId);
        Task<List<ChapterMetadataEntity>> GetChaptersMetadataAsync(int bookId);
        Task<bool> InsertChapterAsync(SaveChapterEntity chapter);
    }
}