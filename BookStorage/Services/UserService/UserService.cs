using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.Dto.UserDto;
using BookStorage.Models.Entities.UserEntities;
using BookStorage.Models.ViewModels.UserViewModel;
using BookStorage.Repositories.UserRepository;

namespace BookStorage.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUserAsync(int id)
        {
            return new UserDto(await _userRepository.GetUserAsync(id));
        }

        public async Task<DataEndpointResultDto<UserDto>> TryCreateUserAsync(RegisterUserViewModel viewModel)
        {
            Dictionary<string, string> errors = new();

            try
            {
                UserEntity createdUser = await _userRepository.UpsertUserAsync(new UserEntity()
                {
                    Username = viewModel.Username,
                    Email = viewModel.Email,
                    Password = viewModel.Password
                });

                if (createdUser == null)
                {
                    return new DataEndpointResultDto<UserDto>(false, null, errors);
                }

                return new DataEndpointResultDto<UserDto>(true, new UserDto(createdUser), errors);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new DataEndpointResultDto<UserDto>(false, null, errors);
            }
        }
    }
}