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

        public async Task<UserProfileViewModel> GetUserProfileViewModel()
        {
            int userId = await GetUserIdAsync();
            UserEntity user = await _userRepository.GetUserAsync(userId);

            return new UserProfileViewModel(user);
        }
    }
}