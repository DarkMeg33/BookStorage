using BookStorage.Models.ViewModels.UserReadingSettingsViewModel;
using BookStorage.Services.UserReadingSettingsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    [Authorize]
    public class UserReadingSettingsController : BaseController
    {
        private readonly IUserReadingSettingsService _userReadingSettingsService;

        public UserReadingSettingsController(IUserReadingSettingsService userReadingSettingsService)
        {
            _userReadingSettingsService = userReadingSettingsService;
        }

        [HttpPost("/user/{userId}/reading-settings")]
        public async Task<IActionResult> UpsertUserReadingSettings(UserReadingSettingsViewModel viewModel, int userId)
        {
            return DynamicResultResponse(
                await _userReadingSettingsService.TryUpsertUserReadingSettingsAsync(viewModel, userId));
        }
    }
}
