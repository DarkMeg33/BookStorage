using BookStorage.Models.Enums;

namespace BookStorage.Models.Entities.UserReadingSettingsEntity
{
    public class UserReadingSettingsEntity
    {
        public int? UserReadingSettingsId { get; set; }
        public int FontSize { get; set; }
        public ReadingThemeMode ThemeMode { get; set; }
        public int UserId { get; set; }
    }
}