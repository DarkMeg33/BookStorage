using BookStorage.Models.Dto.UserDto;
using BookStorage.Models.ViewModels.UserViewModel;
using BookStorage.Services.UserContextService;

namespace BookStorage.Services.UserService
{
    public interface IUserService : IUserContextService
    {
        Task<UserDto> GetUserDtoAsync(int id);
        Task<UserDto> GetUserDtoAsync(string email);
        Task<UserDto> GetUserDtoByUsernameAsync(string username);
        Task<UserProfileViewModel> GetUserProfileViewModel();
    }
}