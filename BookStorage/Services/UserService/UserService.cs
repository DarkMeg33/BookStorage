using BookStorage.Models.Dto.UserDto;
using BookStorage.Repositories.Base;
using BookStorage.Repositories.UserRepository;

namespace BookStorage.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
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
    }
}