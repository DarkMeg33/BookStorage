namespace BookStorage.Models.Entities.BookEntities
{
    public class BookEntity
    {
        public int? BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string CoverStorageReference { get; set; }
        public decimal? Price { get; set; }
    }
}