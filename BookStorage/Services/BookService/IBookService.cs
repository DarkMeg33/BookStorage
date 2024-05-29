using BookStorage.Models.Dto.BookDto;
using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.ViewModels.BookViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Services.BookService
{
    public interface IBookService
    {
        #region Book

        Task<List<BookDto>> GetBookDtosAsync(int currentUserId);
        Task<GetBookDto> GetBookDtoAsync(int bookId, int currentUserId);
        Task<FileResult> GetBookCoverFileAsync(string storageReference);
        Task<BookViewModel> GetBookViewModelAsync(int bookId, int currentUserId);
        Task<List<BookViewModel>> GetBookViewModelsAsync(int currentUserId);
        Task<DataEndpointResultDto<GetBookDto>> TryUpsertBookAsync(FormBookViewModel bookViewModel, int currentUserId);
        Task<EndpointResultDto> TryBuyBookAsync(int bookId, int currentUserId);

        #endregion
    }
}