using BookStorage.Models.ViewModels.UserViewModel;
using BookStorage.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("/user/profile")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> UserProfileView()
        {
            UserProfileViewModel user = await _userService.GetUserProfileViewModel();
            return user == null ? NotFound() : View(user);
        }
    }
}
