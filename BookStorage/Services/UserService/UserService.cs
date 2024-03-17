using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.Dto.UserDto;
using BookStorage.Models.Entities.UserEntities;
using BookStorage.Models.ViewModels.UserViewModel;
using BookStorage.Repositories.Base;
using BookStorage.Repositories.UserRepository;
using BookStorage.Services.ClaimService;

namespace BookStorage.Services.UserService
{
    public class UserService : UserContextService.UserContextService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor, IClaimService claimService) : base(httpContextAccessor, claimService)
        {
            _userRepository = userRepository;

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

        public async Task<UserProfileViewModel> GetUserProfileViewModelAsync()
        {
            int userId = await GetUserIdAsync();
            UserEntity user = await _userRepository.GetUserAsync(userId);

            return new UserProfileViewModel(user);
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
    }
}