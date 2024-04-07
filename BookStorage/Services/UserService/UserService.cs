using BookStorage.Helpers.Formatter;
using BookStorage.Helpers.Mapping;
using BookStorage.Models.Dto.BookDto;
using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.Dto.UserDto;
using BookStorage.Models.Entities.UserEntities;
using BookStorage.Models.ViewModels.BookViewModel;
using BookStorage.Models.ViewModels.UserViewModel;
using BookStorage.Repositories.Base;
using BookStorage.Repositories.UserRepository;
using BookStorage.Services.ClaimService;
using BookStorage.Services.FileStorageService;
using BookStorage.Services.FileValidationService;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Services.UserService
{
    public class UserService : UserContextService.UserContextService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileValidationService _fileValidationService;
        private readonly IFileStorageService _fileStorageService;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor, IClaimService claimService, 
            IFileValidationService fileValidationService,IFileStorageService fileStorageService) : base(httpContextAccessor, claimService)
        {
            _userRepository = userRepository;
            _fileValidationService = fileValidationService;
            _fileStorageService = fileStorageService;

            _userRepository.Attach(unitOfWork);
        }

        public async Task<UserDto> GetUserDtoAsync(int id)
        {
            return new UserDto(await _userRepository.GetUserAsync(id));
        }

        public async Task<UserDto> GetUserDtoAsync(string email)
        {
            return new UserDto(await _userRepository.GetUserAsync(email));
        }

        public async Task<UserDto> GetUserDtoByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUserProfileViewModel> GetUserProfileViewModelAsync()
        {
            int userId = await GetUserIdAsync();
            UserEntity user = await _userRepository.GetUserAsync(userId);

            return new GetUserProfileViewModel(user);
        }

        public async Task<DataEndpointResultDto<UserDto>> UpsertUserProfileAsync(FormUserProfileViewModel viewModel)
        {
            Dictionary<string, string> errors = new();

            try
            {
                int userId = await GetUserIdAsync();

                UserEntity existedUser = await _userRepository.GetUserAsync(userId);

                if (existedUser == null)
                {
                    errors.Add(nameof(existedUser), "Such user doesn't exist");
                    return new DataEndpointResultDto<UserDto>(false, null, errors);
                }

                string oldPassword = await _userRepository.GetUserPasswordAsync(existedUser.Email);

                UserEntity updatedUser = 
                    await _userRepository.UpdateUserProfileAsync(
                        new SaveUserProfileEntity(viewModel, existedUser, oldPassword));

                if (updatedUser == null)
                {
                    return new DataEndpointResultDto<UserDto>(false, null, errors);
                }

                return new DataEndpointResultDto<UserDto>(true, new UserDto(updatedUser), errors);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new DataEndpointResultDto<UserDto>(false, null, errors);
            }
        }

        public async Task<FileResult> GetUserAvatarFileAsync(string storageReference)
        {
            Stream avatarStream =
                await _fileStorageService.GetAvatarAsync(storageReference);

            if (avatarStream == null)
            {
                return null;
            }

            return new FileStreamResult(avatarStream, MimeMapping.GetMimeType(storageReference));
        }

        public async Task<string> GetCurrentUserAvatarUrlAsync()
        {
            int userId = await GetUserIdAsync();

            UserEntity user = await _userRepository.GetUserAsync(userId);

            return user == null
                ? null
                : StringFormatter.ToAvatarUrl(user.AvatarStorageReference);
        }

        public async Task<EndpointResultDto> UpdateUserAvatarAsync(IFormFile avatar)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (!_fileValidationService.IsAvatarValid(avatar, out string errorMessage))
            {
                errors.Add(nameof(avatar), errorMessage);
                return new EndpointResultDto(false, errors);
            }

            await using MemoryStream ms = new MemoryStream();
            await avatar.CopyToAsync(ms);
            byte[] fileContent = ms.ToArray();
            string newStorageReference =
                await _fileStorageService.SaveAvatarAsync(avatar.FileName, fileContent);

            if (string.IsNullOrWhiteSpace(newStorageReference))
            {
                errors.Add(nameof(newStorageReference), "Avatar hasn't been saved");
                return new EndpointResultDto(false, errors);
            }

            UserEntity user = await _userRepository.GetUserAsync(await GetUserIdAsync());

            if (user == null)
            {
                return new EndpointResultDto(false, errors);
            }

            if (!(await _userRepository.UpdateUserAvatarAsync(newStorageReference, user.UserId!.Value)))
            {
                errors.Add(nameof(newStorageReference), "Avatar hasn't been saved");
                return new EndpointResultDto(false, errors);
            }

            string oldStorageReference = user.AvatarStorageReference;

            if (!string.IsNullOrWhiteSpace(oldStorageReference))
            {
                await _fileStorageService.DeleteBookCoverAsync(oldStorageReference);
            }

            return new EndpointResultDto(true, errors);
        }
    }
}