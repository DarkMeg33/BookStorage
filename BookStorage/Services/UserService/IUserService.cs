using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.Dto.UserDto;
using BookStorage.Models.ViewModels.AccountViewModel;

namespace BookStorage.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(int id);
        Task<UserDto> GetUserAsync(string email);
        Task<UserDto> GetUserByUsernameAsync(string username);
    }
}