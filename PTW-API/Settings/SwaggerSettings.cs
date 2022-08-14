namespace PTW_API.Settings
{
    using PTW_API.Contracts;

    public class SwaggerSettings : SettingsBase, ISwaggerSettings
    {
        public SwaggerSettings(IConfiguration configuration)
            : base(configuration) { }

        public string ClientId => GetValue<string>("swagger:clientId");

        public string AuthenticationFlow => GetValue<string>("swagger:authenticationFlow");

        public uint AuthenticationType => GetValue<uint>("swagger:authenticationType");

        public string AuthenticationTypeName => GetValue<string>("swagger:authenticationTypeName");

        public string ApplicationName => GetValue<string>("swagger:applicationName");

        public string Scope => GetValue<string>("swagger:scope");

        public string Description => GetValue<string>("swagger:description");
    }
}
