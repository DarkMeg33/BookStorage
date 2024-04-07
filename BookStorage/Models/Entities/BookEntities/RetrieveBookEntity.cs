namespace BookStorage.Models.Entities.BookEntities
{
    public class RetrieveBookEntity : BookEntity
    {
        public string AuthorName { get; set; }
        public string AuthorAvatarStorageReference { get; set; }
    }
}