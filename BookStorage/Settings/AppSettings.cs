using Newtonsoft.Json;

namespace BookStorage.Settings
{
    public class ConnectionStrings
    {
        [JsonProperty("SqlServer")] public string SqlServer { get; set; }
    }

    public class CookieSettings
    {
        [JsonProperty("CookieName")] public string CookieName { get; set; }
        [JsonProperty("ExpiryAgeInDays")] public int ExpiryAgeInDays { get; set; }
    }

    public class AppSettings
    {
        [JsonProperty("ConnectionStrings")] public ConnectionStrings ConnectionStrings { get; set; }
        [JsonProperty("WebsiteName")] public string WebsiteName { get; set; }
        [JsonProperty("CookieSettings")] public CookieSettings CookieSettings { get; set; }
    }
}