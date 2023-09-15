using BookStorage.Models.Dto.BookDto;

namespace BookStorage.Models.Entities.BookEntities
{
    public class SaveBookEntity : BookEntity
    {
        public SaveBookEntity(BookDto dto)
        {
            BookId = dto.BookId;
            Title = dto.Title;
            Description = dto.Description;
            Author = dto.Author;
        }
    }
}