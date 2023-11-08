using BookStorage.Models.Entities.BookEntities;

namespace BookStorage.Models.Dto.BookDto
{
    public class GetBookDto : BookDto
    {
        public string AuthorName { get; set; }

        public GetBookDto(RetrieveBookEntity entity) : base(entity)
        {
            AuthorName = entity.AuthorName;
        }
    }
}