using BookStorage.Models.Dto.BookDto;
using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.Entities.BookEntities;
using BookStorage.Models.ViewModels.BookViewModel;
using BookStorage.Repositories.Base;
using BookStorage.Repositories.BookRepository;

namespace BookStorage.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;

            _bookRepository.Attach(unitOfWork);
        }

        public async Task<List<BookDto>> GetBookDtosAsync()
        {
            return (await _bookRepository.GetBooksAsync())
                .Select(x => new BookDto(x))
                .ToList();
        }

        public async Task<GetBookDto> GetBookDtoAsync(int bookId)
        {
            return new GetBookDto(await _bookRepository.GetBookAsync(bookId));
        }

        public async Task<BookViewModel> GetBookViewModelAsync(int bookId)
        {
            return new BookViewModel(await _bookRepository.GetBookAsync(bookId));
        }

        public async Task<DataEndpointResultDto<GetBookDto>> TryUpsertBookAsync(FormBookViewModel bookViewModel, int currentUserId)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            //TODO migrate this to validate model attribute
            RetrieveBookEntity existingBook = await _bookRepository.GetBookAsync(bookViewModel.Title);

            if (existingBook != null)
            {
                errors.Add(nameof(existingBook), "There is book with such title");
                return new DataEndpointResultDto<GetBookDto>(false, null, errors);
            }

            try
            {
                RetrieveBookEntity upsertedBook 
                    = await _bookRepository.UpsertBookAsync(new SaveBookEntity(bookViewModel, currentUserId));

                if (upsertedBook == null)
                {
                    return new DataEndpointResultDto<GetBookDto>(false, null, errors);
                }

                return new DataEndpointResultDto<GetBookDto>(true, new GetBookDto(upsertedBook), errors);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new DataEndpointResultDto<GetBookDto>(false, null, errors);
            }
        }
    }
}