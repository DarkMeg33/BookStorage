using BookStorage.Helpers.Mapping;
using BookStorage.Models.Dto.BookDto;
using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.Dto.FictionBookDto;
using BookStorage.Models.Entities.BookEntities;
using BookStorage.Models.Entities.ChapterEntities;
using BookStorage.Models.Entities.UserEntities;
using BookStorage.Models.ViewModels.BookViewModel;
using BookStorage.Repositories.Base;
using BookStorage.Repositories.BookRepository;
using BookStorage.Repositories.ChapterRepository;
using BookStorage.Repositories.UserRepository;
using BookStorage.Services.ChapterService;
using BookStorage.Services.FictionBookReaderService;
using BookStorage.Services.FileStorageService;
using BookStorage.Services.FileValidationService;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IFileValidationService _fileValidationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFictionBookReaderService _fictionBookReaderService;
        private readonly IChapterRepository _chapterRepository;
        private readonly IUserRepository _userRepository;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork, 
            IFileStorageService fileStorageService, IFileValidationService fileValidationService, IFictionBookReaderService fictionBookReaderService, IChapterService chapterService, IChapterRepository chapterRepository, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
            _fileValidationService = fileValidationService;
            _fictionBookReaderService = fictionBookReaderService;
            _chapterRepository = chapterRepository;
            _userRepository = userRepository;

            _bookRepository.Attach(unitOfWork);
            _chapterRepository.Attach(unitOfWork);
            _userRepository.Attach(unitOfWork);
        }

        #region Book

        public async Task<List<BookDto>> GetBookDtosAsync(int currentUserId)
        {
            return (await _bookRepository.GetBooksAsync(currentUserId))
                .Select(x => new BookDto(x))
                .ToList();
        }

        public async Task<GetBookDto> GetBookDtoAsync(int bookId, int currentUserId)
        {
            return new GetBookDto(await _bookRepository.GetBookAsync(bookId, currentUserId));
        }

        public async Task<FileResult> GetBookCoverFileAsync(string storageReference)
        {
            Stream bookCoverStream = 
                await _fileStorageService.GetBookCoverAsync(storageReference);

            if (bookCoverStream == null)
            {
                return null;
            }
            
            return new FileStreamResult(bookCoverStream, MimeMapping.GetMimeType(storageReference));
        }

        public async Task<BookViewModel> GetBookViewModelAsync(int bookId, int currentUserId)
        {
            return new BookViewModel(await _bookRepository.GetBookAsync(bookId, currentUserId));
        }

        public async Task<List<BookViewModel>> GetBookViewModelsAsync(int currentUserId)
        {
            return (await _bookRepository.GetBooksAsync(currentUserId))
                .OrderByDescending(x => x.BookId)
                .Select(x => new BookViewModel(x))
                .ToList();
        }

        public async Task<DataEndpointResultDto<GetBookDto>> TryUpsertBookAsync(FormBookViewModel bookViewModel, int currentUserId)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            //TODO migrate this to validate model attribute
            RetrieveBookEntity existingBook = await _bookRepository.GetBookAsync(bookViewModel.Title, currentUserId);

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

                FictionBookDto fictionBook = await _fictionBookReaderService.ReadDocumentAsync(bookViewModel.BookFile);

                if (fictionBook == null)
                {
                    _unitOfWork.RollBack();
                    return new DataEndpointResultDto<GetBookDto>(false, null, errors);
                }

                bool chaptersInsertResult = 
                    await InsertChaptersAsync(fictionBook.Chapters, upsertedBook.BookId.Value);

                if (!chaptersInsertResult)
                {
                    _unitOfWork.RollBack();
                    return new DataEndpointResultDto<GetBookDto>(false, null, errors);
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

        #region TryUpsertBookAsyncPrivate

        private async Task<bool> InsertChaptersAsync(List<FictionBookChapterDto> chapters, int bookId)
        {
            foreach (FictionBookChapterDto chapter in chapters)
            {
                bool result = await _chapterRepository.InsertChapterAsync(new SaveChapterEntity()
                {
                    ChapterId = null,
                    BookId = bookId,
                    Title = chapter.Title,
                    Content = chapter.Content
                });

                if (!result)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        public async Task<EndpointResultDto> TryBuyBookAsync(int bookId, int currentUserId)
        {
            RetrieveBookEntity book = await _bookRepository.GetBookAsync(bookId, currentUserId);

            if (book == null)
            {
                return new EndpointResultDto(false, null);
            }

            RetrieveUserEntity author = await _userRepository.GetUserAsync(book.AuthorId);

            _unitOfWork.Begin();

            try
            {
                if (!(await _bookRepository.InsertUserBoughtBookAsync(bookId, currentUserId)))
                {
                    _unitOfWork.RollBack();
                    return new EndpointResultDto(false, null);
                }

                if (!(await _userRepository.SetUserBalanceAsync(author.UserId!.Value, author.Balance + book.Price!.Value)))
                {
                    _unitOfWork.RollBack();
                    return new EndpointResultDto(false, null);
                }

                _unitOfWork.Commit();
                return new EndpointResultDto(true, null);
            }
            catch (Exception e)
            {
                _unitOfWork.RollBack();
                Console.WriteLine(e);
                return new EndpointResultDto(false, null);
            }
        }

        #endregion
    }
}