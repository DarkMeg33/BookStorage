namespace BookStorage.Models.Entities.ChapterEntities
{
    public class ChapterEntity
    {
        public int? ChapterId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BookId { get; set; }
    }
}