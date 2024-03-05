using BookStorage.Models.Dto.BookDto;
using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.ViewModels.BookViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Services.BookService
{
    public interface IBookService
    {
        #region Book

        Task<List<BookDto>> GetBookDtosAsync();
        Task<GetBookDto> GetBookDtoAsync(int bookId);
        Task<FileResult> GetBookCoverFileAsync(string storageReference);
        Task<BookViewModel> GetBookViewModelAsync(int bookId);
        Task<DataEndpointResultDto<GetBookDto>> TryUpsertBookAsync(FormBookViewModel bookViewModel, int currentUserId);

        #endregion
    }
}