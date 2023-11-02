using BookStorage.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BookStorage.Extensions
{
    public static class AuthenticationExtensions
    {
        public static void SetupCookie(this IServiceCollection services, AppSettings appSettings)
        {
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie((options) =>
                {
                    options.LoginPath = "/account/login";
                    options.AccessDeniedPath = "/access-denied";
                    options.ReturnUrlParameter = "returnUrl";
                    options.Cookie = new CookieBuilder()
                    {
                        Name = appSettings.CookieSettings.CookieName,
                        MaxAge = TimeSpan.FromDays(appSettings.CookieSettings.ExpiryAgeInDays)
                    };
                });
        }
    }
}