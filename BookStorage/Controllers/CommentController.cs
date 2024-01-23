using BookStorage.Models.Dto.CommentDto;
using BookStorage.Models.ViewModels.CommentViewModel;
using BookStorage.Services.CommentService;
using BookStorage.Services.UserContextService;
using BookStorage.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly IUserContextService _userContextService;

        public CommentController(ICommentService commentService, IUserContextService userContextService)
        {
            _commentService = commentService;
            _userContextService = userContextService;
        }

        [HttpGet("/book/{bookId}/comments")]
        public async Task<IActionResult> GetComments(int bookId)
        {
            return Json(await _commentService.GetCommentsAsync(bookId));
        }

        [HttpPost("/book/{bookId}/comment")]
        public async Task<IActionResult> UpsertCommentAsync(CommentViewModel commentViewModel, int bookId)
        {
            return DynamicResultResponse(
                await _commentService.UpsertCommentAsync(commentViewModel, bookId, 
                    await _userContextService.GetUserIdAsync()));
        }
    }
}
