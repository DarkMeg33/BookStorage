namespace BookStorage.Models.ViewModels.BookViewModel
{
    public class FormBookViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile BookCoverImage { get; set; }
        public IFormFile BookFile { get; set; }
    }
}