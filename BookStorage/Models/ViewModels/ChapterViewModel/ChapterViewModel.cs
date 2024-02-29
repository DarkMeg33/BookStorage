using BookStorage.Models.Entities.ChapterEntities;

namespace BookStorage.Models.ViewModels.ChapterViewModel
{
    public class ChapterViewModel
    {
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }

        public ChapterViewModel(ChapterMetadataEntity entity)
        {
            ChapterId = entity.ChapterId;
            Title = entity.Title;
            CreatedAt = entity.CreatedAt;
        }
    }
}