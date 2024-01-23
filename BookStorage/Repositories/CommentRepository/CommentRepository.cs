using BookStorage.Models.Entities.CommentEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.CommentRepository
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public async Task<List<RetrieveCommentEntity>> GetCommentsAsync(int bookId)
        {
            return await GetAllAsync<RetrieveCommentEntity>("Comment_Select", new { bookId });
        }

        public async Task<RetrieveCommentEntity> UpsertCommentAsync(SaveCommentEntity entity)
        {
            return await GetAsync<RetrieveCommentEntity>("Comment_Upsert", entity);
        }
    }
}