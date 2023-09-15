using Newtonsoft.Json;

namespace BookStorage.Settings
{
    public class ConnectionStrings
    {
        [JsonProperty("SqlServer")] public string SqlServer { get; set; }
    }

    public class AppSettings
    {
        [JsonProperty("ConnectionStrings")] public ConnectionStrings ConnectionStrings { get; set; }
    }
}