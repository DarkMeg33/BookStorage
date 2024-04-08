using BookStorage.Models.Entities.ChapterEntities;
using BookStorage.Models.Entities.UserReadingSettingsEntity;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.UserReadingSettingsRepository
{
    public interface IUserReadingSettingsRepository : IRepository
    {
        Task<UserReadingSettingsEntity> GetUserReadingSettingsAsync(int userId);
        Task<UserReadingSettingsEntity> UpsertUserReadingSettingsAsync(UserReadingSettingsEntity settings);
    }
}