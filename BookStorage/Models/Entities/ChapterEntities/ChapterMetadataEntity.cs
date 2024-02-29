namespace BookStorage.Models.Entities.ChapterEntities
{
    public class ChapterMetadataEntity
    {
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}