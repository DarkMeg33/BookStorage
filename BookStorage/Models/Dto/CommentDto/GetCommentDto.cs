using BookStorage.Models.Entities.CommentEntities;

namespace BookStorage.Models.Dto.CommentDto
{
    public class GetCommentDto : CommentDto
    {
        public string AuthorName { get; set; }

        public GetCommentDto(RetrieveCommentEntity entity) : base(entity)
        {
            AuthorName = entity.AuthorName;
        }
    }
}