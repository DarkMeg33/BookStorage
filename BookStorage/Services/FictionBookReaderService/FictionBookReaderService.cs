using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using BookStorage.Extensions;
using BookStorage.Helpers.Formatter;
using BookStorage.Models.Dto.FictionBookDto;

namespace BookStorage.Services.FictionBookReaderService
{
    public class FictionBookReaderService : IFictionBookReaderService
    {
        public async Task<FictionBookDto> ReadDocumentAsync(IFormFile file)
        {
            FictionBookDto fictionBook = new FictionBookDto();

            await using Stream stream = file.OpenReadStream();

            try
            {
                XDocument document = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
                
                XElement root = document.Root.RemoveAllNamespaces();

                XElement description = root?.XPathSelectElement("description");

                fictionBook.Title = 
                    description?.XPathSelectElement("//title-info/book-title")?.Value;
                fictionBook.Author = StringFormatter.FormatName(
                    description?.XPathSelectElement("//title-info/author/first-name")?.Value,
                    description?.XPathSelectElement("//title-info/author/last-name")?.Value);
                fictionBook.Genre = description?.XPathSelectElement("//title-info/genre")?.Value;
                fictionBook.Annotation = string.Join("", 
                    description?.XPathSelectElements("//title-info/annotation/p")!);

                XElement body = root?.XPathSelectElement("body");

                List<XElement> chapters = body?.XPathSelectElements("section").ToList();

                fictionBook.Chapters = new List<FictionBookChapterDto>();

                foreach (XElement chapter in chapters)
                {
                    FictionBookChapterDto fictionBookChapter = new FictionBookChapterDto();
                    fictionBookChapter.Title = chapter.XPathSelectElement("title")?.Value;
                    fictionBookChapter.Content = string.Join("", chapter.XPathSelectElements("p"));

                    fictionBook.Chapters.Add(fictionBookChapter);
                }

                return fictionBook;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}