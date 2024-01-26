using BookStorage.Models.Dto.CommentDto;
using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.ViewModels.CommentViewModel;

namespace BookStorage.Services.CommentService
{
    public interface ICommentService
    {
        Task<List<GetCommentDto>> GetCommentDtosAsync(int bookId);
        Task<List<CommentViewModel>> GetCommentViewModelsAsync(int bookId);
        Task<DataEndpointResultDto<GetCommentDto>> TryUpsertCommentAsync(NewCommentViewModel viewModel, int bookId,
            int currentUserId);
    }
}