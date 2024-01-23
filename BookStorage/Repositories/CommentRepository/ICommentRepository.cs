using BookStorage.Models.Entities.CommentEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.CommentRepository
{
    public interface ICommentRepository : IRepository
    {
        Task<List<RetrieveCommentEntity>> GetCommentsAsync(int bookId);
        Task<RetrieveCommentEntity> UpsertCommentAsync(SaveCommentEntity entity);
    }
}