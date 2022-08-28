namespace PTW_API.Services
{
    using PTW.Domain.Storage.Forecast.Repositories.Abstractions;
    using PTW.Domain.Storage.Forecast.Repositories.Implementations;

    public static class RegisterServices
    {
        public static IServiceCollection AddPTWServices(this IServiceCollection services)
        {
            services.AddScoped<IForecastRepository, ForecastRepository>();

            return services;
        }
    }
}
