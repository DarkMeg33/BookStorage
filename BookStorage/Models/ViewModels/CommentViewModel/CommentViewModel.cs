using BookStorage.Models.Entities.CommentEntities;

namespace BookStorage.Models.ViewModels.CommentViewModel
{
    public class CommentViewModel
    {
        public string AuthorName { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public CommentViewModel(RetrieveCommentEntity entity)
        {
            AuthorName = entity.AuthorName;
            Text = entity.Text;
            CreatedAt = entity.CreatedAt;
        }
    }
}