namespace PTW_API.Settings
{
    using PTW_API.Contracts;

    public class OAuthSettings : SettingsBase, IOAuthSettings
    {
        public OAuthSettings(IConfiguration configuration)
            : base(configuration) { }

        public Uri AuthorizeUrl => new Uri(GetValue<string>("oauth:authorizeUrl"));
    }
}
