using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.ViewModels.UserReadingSettingsViewModel;

namespace BookStorage.Services.UserReadingSettingsService
{
    public interface IUserReadingSettingsService
    {
        Task<UserReadingSettingsViewModel> GetUserReadingSettingsViewModelAsync(int userId);
        Task<DataEndpointResultDto<UserReadingSettingsViewModel>> TryUpsertUserReadingSettingsAsync(
            UserReadingSettingsViewModel settings, int userId);
    }
}