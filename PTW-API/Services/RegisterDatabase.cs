namespace PTW_API.Services
{
    using PTW_API.Contracts;
    using Microsoft.EntityFrameworkCore;
    using PTW.Domain.Storage.Common.Context;

    public static class RegisterDatabase
    {
        public static IServiceCollection RegisterPTWDatabase(this IServiceCollection services)
        {
            IAppSettings appSettings = services.BuildServiceProvider().GetRequiredService<IAppSettings>();

            IConnectionStringSettings connectionStringSettings = services.BuildServiceProvider().GetRequiredService<IConnectionStringSettings>();

            services.AddScoped<IPTWDbContext, PTWDbContext>();

            services.AddDbContext<PTWDbContext>(options =>
            {
                options.UseMySql(connectionStringSettings.PTWSqlDb,
                                 ServerVersion.AutoDetect(connectionStringSettings.PTWSqlDb),
                                 mysqlOptions =>
                                 {
                                     mysqlOptions.EnableRetryOnFailure(
                                     maxRetryCount: 5,
                                     maxRetryDelay: TimeSpan.FromSeconds(15),
                                     errorNumbersToAdd: null);
                                 });
            });

            return services;
        }
    }
}
