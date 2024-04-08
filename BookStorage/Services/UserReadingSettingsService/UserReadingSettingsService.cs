using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.Entities.UserReadingSettingsEntity;
using BookStorage.Models.ViewModels.UserReadingSettingsViewModel;
using BookStorage.Repositories.Base;
using BookStorage.Repositories.UserReadingSettingsRepository;

namespace BookStorage.Services.UserReadingSettingsService
{
    public class UserReadingSettingsService : IUserReadingSettingsService
    {
        private readonly IUserReadingSettingsRepository _userReadingSettingsRepository;

        public UserReadingSettingsService(IUserReadingSettingsRepository userReadingSettingsRepository, IUnitOfWork unitOfWork)
        {
            _userReadingSettingsRepository = userReadingSettingsRepository;

            _userReadingSettingsRepository.Attach(unitOfWork);
        }

        public async Task<UserReadingSettingsViewModel> GetUserReadingSettingsViewModelAsync(int userId)
        {
            UserReadingSettingsEntity entity = await _userReadingSettingsRepository.GetUserReadingSettingsAsync(userId);

            return entity == null ? null : new UserReadingSettingsViewModel(entity);
        }

        public async Task<DataEndpointResultDto<UserReadingSettingsViewModel>> TryUpsertUserReadingSettingsAsync(
            UserReadingSettingsViewModel settings, int userId)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            try
            {
                UserReadingSettingsEntity upserted = await _userReadingSettingsRepository.UpsertUserReadingSettingsAsync(new UserReadingSettingsEntity
                {
                    UserReadingSettingsId = settings.UserReadingSettingsId,
                    FontSize = settings.FontSize,
                    ThemeMode = settings.ThemeMode,
                    UserId = userId
                });

                if (upserted == null)
                {
                    return new DataEndpointResultDto<UserReadingSettingsViewModel>(false, null, errors);
                }

                return new DataEndpointResultDto<UserReadingSettingsViewModel>(true, new UserReadingSettingsViewModel(upserted), errors);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new DataEndpointResultDto<UserReadingSettingsViewModel>(false, null, errors);
            }
        }
    }
}