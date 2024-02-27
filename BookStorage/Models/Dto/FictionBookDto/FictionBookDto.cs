namespace BookStorage.Models.Dto.FictionBookDto
{
    public class FictionBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Annotation { get; set; }
        public List<FictionBookChapterDto> Chapters { get; set; }
    }
}