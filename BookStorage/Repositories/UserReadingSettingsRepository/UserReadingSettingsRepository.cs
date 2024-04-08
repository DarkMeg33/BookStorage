using BookStorage.Models.Entities.ChapterEntities;
using BookStorage.Models.Entities.UserReadingSettingsEntity;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.UserReadingSettingsRepository
{
    public class UserReadingSettingsRepository : BaseRepository, IUserReadingSettingsRepository
    {
        public async Task<UserReadingSettingsEntity> GetUserReadingSettingsAsync(int userId)
        {
            return await GetAsync<UserReadingSettingsEntity>("UserReadingSettings_Select", new { userId });
        }

        public async Task<UserReadingSettingsEntity> UpsertUserReadingSettingsAsync(UserReadingSettingsEntity settings)
        {
            return await GetAsync<UserReadingSettingsEntity>("UserReadingSettings_Upsert", settings);
        }
    }
}