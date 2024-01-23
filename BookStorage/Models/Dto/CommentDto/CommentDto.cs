using BookStorage.Models.Entities.CommentEntities;

namespace BookStorage.Models.Dto.CommentDto
{
    public class CommentDto
    {
        public int? CommentId { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public DateTime CreatedAt { get; set; }

        public CommentDto(RetrieveCommentEntity entity)
        {
            CommentId = entity.CommentId;
            Text = entity.Text;
            AuthorId = entity.AuthorId;
            BookId = entity.BookId;
            CreatedAt = entity.CreatedAt;
        }
    }
}