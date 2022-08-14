namespace PTW_API.Settings
{
    using PTW_API.Contracts;

    public class AppSettings : SettingsBase, IAppSettings
    {
        private ApiVersionSettings version1_0;

        public AppSettings(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Gets the api version settings.
        /// </summary>
        public ApiVersionSettings Version1_0 => version1_0 ?? (version1_0 = GetSection<ApiVersionSettings>("apiDocs:version1_0"));
    }
}
