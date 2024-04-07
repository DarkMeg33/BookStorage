using System.Threading.Tasks;
using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.Dto.UserDto;
using BookStorage.Models.ViewModels.UserViewModel;
using BookStorage.Services.UserContextService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Services.UserService
{
    public interface IUserService : IUserContextService
    {
        Task<UserDto> GetUserDtoAsync(int id);
        Task<UserDto> GetUserDtoAsync(string email);
        Task<UserDto> GetUserDtoByUsernameAsync(string username);
        Task<UserProfileViewModel> GetUserProfileViewModelAsync();
        Task<DataEndpointResultDto<UserDto>> UpsertUserProfileAsync(FormUserProfileViewModel viewModel);
        Task<FileResult> GetUserAvatarFileAsync(string storageReference);
        Task<string> GetCurrentUserAvatarUrlAsync();
        Task<string> GetUserAvatarUrlAsync(int userId);
        Task<EndpointResultDto> UpdateUserAvatarAsync(IFormFile avatar);
    }
}