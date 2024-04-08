using BookStorage.Models.Entities.UserReadingSettingsEntity;
using BookStorage.Models.Enums;

namespace BookStorage.Models.ViewModels.UserReadingSettingsViewModel
{
    public class UserReadingSettingsViewModel
    {
        public int? UserReadingSettingsId { get; set; }
        public int FontSize { get; set; }
        public ReadingThemeMode ThemeMode { get; set; }

        public UserReadingSettingsViewModel() { }

        public UserReadingSettingsViewModel(UserReadingSettingsEntity e)
        {
            UserReadingSettingsId = e.UserReadingSettingsId;
            FontSize = e.FontSize;
            ThemeMode = e.ThemeMode;
        }
    }
}