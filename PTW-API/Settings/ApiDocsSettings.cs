namespace PTW_API.Settings
{
    using PTW_API.Contracts;

    public class ApiDocsSettings : SettingsBase, IApiDocsSettings
    {
        public ApiDocsSettings(IConfiguration configuration)
            : base(configuration) { }

        public string RoutePrefix => GetValue<string>("apiDocs:routePrefix");

        public string RouteTemplate => GetValue<string>("apiDocs:routeTemplate");
    }
}
