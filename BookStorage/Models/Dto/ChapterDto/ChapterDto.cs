using BookStorage.Models.Entities.ChapterEntities;

namespace BookStorage.Models.Dto.ChapterDto
{
    public class ChapterDto
    {
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BookId { get; set; }

        public ChapterDto(RetrieveChapterEntity entity)
        {
            ChapterId = entity.ChapterId!.Value;
            Title = entity.Title;
            Content = entity.Content;
            BookId = entity.BookId;
        }
    }
}