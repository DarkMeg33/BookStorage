using BookStorage.Models.Entities.ChapterEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.ChapterRepository
{
    public class ChapterRepository : BaseRepository, IChapterRepository
    {
        public async Task<List<RetrieveChapterEntity>> GetChaptersAsync(int bookId)
        {
            return await GetAllAsync<RetrieveChapterEntity>("Chapter_Select", new { bookId });
        }

        public async Task<RetrieveChapterEntity> GetChapterAsync(int chapterId)
        {
            return await GetAsync<RetrieveChapterEntity>("Chapter_Select", new { chapterId });
        }

        public async Task<ChapterMetadataEntity> GetChapterMetadataAsync(int chapterId)
        {
            return await GetAsync<ChapterMetadataEntity>("Chapter_SelectMetadata", new { chapterId });
        }

        public async Task<List<ChapterMetadataEntity>> GetChaptersMetadataAsync(int bookId)
        {
            return await GetAllAsync<ChapterMetadataEntity>("Chapter_SelectMetadata", new { bookId });
        }

        public async Task<bool> InsertChapterAsync(SaveChapterEntity chapter)
        {
            return await ExecuteAsync("Chapter_Insert", chapter, commandTimeout: 60);
        }
    }
}