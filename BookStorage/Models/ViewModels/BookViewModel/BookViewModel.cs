using BookStorage.Models.Entities.BookEntities;

namespace BookStorage.Models.ViewModels.BookViewModel
{
    public class BookViewModel
    {
        public int? BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        public BookViewModel(RetrieveBookEntity entity)
        {
            BookId = entity.BookId;
            Title = entity.Title;
            Description = entity.Description;
            AuthorId = entity.AuthorId;
            AuthorName = entity.AuthorName;
        }
    }
}