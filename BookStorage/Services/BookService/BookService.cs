using BookStorage.Models.Dto.BookDto;
using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.Entities.BookEntities;
using BookStorage.Models.ViewModels.BookViewModel;
using BookStorage.Repositories.Base;
using BookStorage.Repositories.BookRepository;
using BookStorage.Services.FileStorageService;
using BookStorage.Services.FileValidationService;

namespace BookStorage.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IFileValidationService _fileValidationService;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork, 
            IFileStorageService fileStorageService, IFileValidationService fileValidationService)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
            _fileValidationService = fileValidationService;

            _bookRepository.Attach(unitOfWork);
        }

        #region Book

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

            _unitOfWork.Begin();

            try
            {
                RetrieveBookEntity upsertedBook 
                    = await _bookRepository.UpsertBookAsync(new SaveBookEntity(bookViewModel, currentUserId));

                if (upsertedBook == null)
                {
                    _unitOfWork.RollBack();
                    return new DataEndpointResultDto<GetBookDto>(false, null, errors);
                }

                if (!_fileValidationService.IsBookCoverValid(bookViewModel.BookCoverImage, out string errorMessage))
                {
                    errors.Add(nameof(bookViewModel.BookCoverImage), errorMessage);
                    return new DataEndpointResultDto<GetBookDto>(false, null, errors);
                }

                await using MemoryStream ms = new MemoryStream();
                await bookViewModel.BookCoverImage.CopyToAsync(ms);
                byte[] fileContent = ms.ToArray();

                string newStorageReference = 
                    await _fileStorageService.SaveBookCoverAsync(bookViewModel.BookCoverImage.FileName, fileContent);

                if (string.IsNullOrWhiteSpace(newStorageReference))
                {
                    _unitOfWork.RollBack();
                    errors.Add(nameof(newStorageReference), "Cover hasn't been saved");
                    return new DataEndpointResultDto<GetBookDto>(false, null, errors);
                }

                if (!(await _bookRepository.UpdateBookCoverAsync(upsertedBook.BookId!.Value, newStorageReference)))
                {
                    _unitOfWork.RollBack();
                    errors.Add(nameof(newStorageReference), "Cover hasn't been saved");
                    return new DataEndpointResultDto<GetBookDto>(false, null, errors);
                }

                string oldStorageReference = upsertedBook.CoverStorageReference;

                if (!string.IsNullOrWhiteSpace(oldStorageReference))
                {
                    await _fileStorageService.DeleteBookCoverAsync(oldStorageReference);
                }

                _unitOfWork.Commit();
                return new DataEndpointResultDto<GetBookDto>(true, new GetBookDto(upsertedBook), errors);
            }
            catch (Exception e)
            {
                _unitOfWork.RollBack();
                Console.WriteLine(e);
                return new DataEndpointResultDto<GetBookDto>(false, null, errors);
            }
        }

        #endregion
    }
}