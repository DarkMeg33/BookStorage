using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.Dto.UserDto;
using BookStorage.Models.ViewModels.UserViewModel;

namespace BookStorage.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(int id);
        Task<DataEndpointResultDto<UserDto>> TryCreateUserAsync(RegisterUserViewModel viewModel);
    }
}