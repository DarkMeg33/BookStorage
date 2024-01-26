using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.Dto.UserDto;
using BookStorage.Models.ViewModels.AccountViewModel;

namespace BookStorage.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> GetUserDtoAsync(int id);
        Task<UserDto> GetUserDtoAsync(string email);
        Task<UserDto> GetUserDtoByUsernameAsync(string username);
    }
}