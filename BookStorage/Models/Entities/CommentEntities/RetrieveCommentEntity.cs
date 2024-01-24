namespace BookStorage.Models.Entities.CommentEntities
{
    public class RetrieveCommentEntity : CommentEntity
    {
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; }
    }
}