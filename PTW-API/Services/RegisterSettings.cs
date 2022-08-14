namespace PTW_API.Services
{
    using PTW_API.Contracts;
    using PTW_API.Settings;

    public static class RegisterSettings
    {
        public static IServiceCollection AddPTWSettings(this IServiceCollection services, 
                                                        IConfiguration configuration, 
                                                        IWebHostEnvironment environment)
        {
            services.AddSingleton<IAppSettings, AppSettings>();

            services.AddSingleton<IApiDocsSettings>(provider => new ApiDocsSettings(configuration));

            services.AddSingleton<ISwaggerSettings>(provider => new SwaggerSettings(configuration));

            services.AddSingleton<IOAuthSettings>(provider => new OAuthSettings(configuration));

            services.AddSingleton<ISecretsManagerSettings>(provider => new SecretsManagerSettings(configuration, environment));

            return services;
        }
    }
}
