using BookStorage.Models.ViewModels.UserViewModel;
using BookStorage.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/user/profile")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> UserProfileView()
        {
            GetUserProfileViewModel user = await _userService.GetUserProfileViewModelAsync();
            return user == null ? NotFound() : View(user);
        }
        
        [HttpPost("/user/profile")]
        public async Task<IActionResult> SaveUserProfile(FormUserProfileViewModel viewModel)
        {
            return DynamicResultResponse(await _userService.UpsertUserProfileAsync(viewModel));
        }
        
        [HttpGet("/user/avatar/{storageReference}")]
        public async Task<IActionResult> GetUserAvatar(string storageReference)
        {
            return await _userService.GetUserAvatarFileAsync(storageReference);
        }
        
        [HttpPost("/user/avatar")]
        public async Task<IActionResult> UpdateUserAvatar()
        {
            return DynamicResultResponse(await _userService.UpdateUserAvatarAsync(Request.Form.Files.FirstOrDefault()));
        }
    }
}
