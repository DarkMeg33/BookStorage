using BookStorage.Helpers.Formatter;
using BookStorage.Models.Entities.CommentEntities;

namespace BookStorage.Models.Dto.CommentDto
{
    public class GetCommentDto : CommentDto
    {
        public string AuthorName { get; set; }
        public string AuthorAvatarUrl { get; set; }

        public GetCommentDto(RetrieveCommentEntity entity) : base(entity)
        {
            AuthorName = entity.AuthorName;
            AuthorAvatarUrl = StringFormatter.ToAvatarUrl(entity.AuthorAvatarStorageReference);
        }
    }
}