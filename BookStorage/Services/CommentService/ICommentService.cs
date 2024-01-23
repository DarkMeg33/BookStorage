using BookStorage.Models.Dto.CommentDto;
using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.ViewModels.CommentViewModel;

namespace BookStorage.Services.CommentService
{
    public interface ICommentService
    {
        Task<List<GetCommentDto>> GetCommentsAsync(int bookId);
        Task<DataEndpointResultDto<GetCommentDto>> UpsertCommentAsync(CommentViewModel viewModel, int bookId,
            int currentUserId);
    }
}