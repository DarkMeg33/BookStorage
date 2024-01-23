namespace BookStorage.Models.Entities.CommentEntities
{
    public class CommentEntity
    {
        public int? CommentId { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; } 
        public int BookId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}