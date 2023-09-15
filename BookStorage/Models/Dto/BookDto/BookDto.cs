using BookStorage.Models.Entities.BookEntities;

namespace BookStorage.Models.Dto.BookDto
{
    public class BookDto
    {
        public int? BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Author { get; set; }

        public BookDto(RetrieveBookEntity entity)
        {
            BookId = entity.BookId;
            Title = entity.Title;
            Description = entity.Description;
            Author = entity.Author;
        }
    }
}