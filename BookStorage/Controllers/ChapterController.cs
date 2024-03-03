using BookStorage.Models.Dto.ChapterDto;
using BookStorage.Services.ChapterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    public class ChapterController : BaseController
    {
        private readonly IChapterService _chapterService;

        public ChapterController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("/book/{bookId}/chapter/{chapterId}/view")]
        public async Task<IActionResult> ChapterView(int chapterId)
        {
            ChapterDto chapter = await _chapterService.GetChapterDtoAsync(chapterId);

            return chapter == null ? NotFound() : View("ChapterView", chapter);
        }
    }
}
