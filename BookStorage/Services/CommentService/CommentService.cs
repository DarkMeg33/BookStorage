using BookStorage.Models.Dto.CommentDto;
using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.Entities.CommentEntities;
using BookStorage.Models.ViewModels.CommentViewModel;
using BookStorage.Repositories.Base;
using BookStorage.Repositories.CommentRepository;

namespace BookStorage.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;

            _commentRepository.Attach(unitOfWork);
        }

        public async Task<List<GetCommentDto>> GetCommentDtosAsync(int bookId)
        {
            return (await _commentRepository.GetCommentsAsync(bookId))
                .Select(e => new GetCommentDto(e))
                .ToList();
        }

        public async Task<List<CommentViewModel>> GetCommentViewModelsAsync(int bookId)
        {
            return (await _commentRepository.GetCommentsAsync(bookId))
                .Select(e => new CommentViewModel(e))
                .ToList();
        }

        public async Task<DataEndpointResultDto<GetCommentDto>> TryUpsertCommentAsync(NewCommentViewModel viewModel, 
            int bookId, int currentUserId)
        {
            Dictionary<string, string> errors = new();

            try
            {
                RetrieveCommentEntity upsertedEntity = 
                    await _commentRepository.UpsertCommentAsync(new SaveCommentEntity()
                    {
                        Text = viewModel.Text,
                        BookId = bookId,
                        AuthorId = currentUserId
                    });

                if (upsertedEntity == null)
                {
                    return new DataEndpointResultDto<GetCommentDto>(false, null, errors);
                }

                return new DataEndpointResultDto<GetCommentDto>(true, new GetCommentDto(upsertedEntity), errors);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new DataEndpointResultDto<GetCommentDto>(false, null, errors);
            }
        }
    }
}