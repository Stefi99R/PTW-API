using PTW_API.Contracts;

namespace PTW_API.Settings
{
    public class ConnectionStringSettings : SettingsBase, IConnectionStringSettings
    {
        public ConnectionStringSettings(IConfiguration configuration)
            : base(configuration) { }

        public string PTWSqlDb => GetValue<string>("ConnectionStrings:PTWSqlDb");
    }
}
