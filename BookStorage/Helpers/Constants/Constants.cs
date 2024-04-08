using BookStorage.Models.Enums;
using BookStorage.Models.ViewModels.UserReadingSettingsViewModel;

namespace BookStorage.Helpers.Constants
{
    public static class Constants
    {
        public static string DefaultCoverUrl => "/images/book/book-img.jpg";
        public static string DefaultAvatarUrl => "/images/avatar/small/joe.jpg";
        public static UserReadingSettingsViewModel DefaultUserReadingSettings { get; }

        static Constants() {
            DefaultUserReadingSettings = new UserReadingSettingsViewModel
            {
                UserReadingSettingsId = null,
                FontSize = 14,
                ThemeMode = ReadingThemeMode.Light
            };
        }
    }
}