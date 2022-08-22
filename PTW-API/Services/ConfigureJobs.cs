namespace PTW_API.Services
{
    using Hangfire;
    using Hangfire.MySql;
    using Newtonsoft.Json;
    using PTW_API.Contracts;
    using PTW_API.Filters;
    using TimeZoneConverter;

    public static class ConfigureJobs
    {
        private const string _centralEuropeanTimeZoneId = "Central European Standard Time";

        public static IServiceCollection AddJobsConfig(this IServiceCollection services)
        {
            IServiceProvider provider = services.BuildServiceProvider();

            IJobsSettings jobsSettings = provider.GetService<IJobsSettings>();

            MySqlStorageOptions mysqlStorageOptions = new MySqlStorageOptions
            {
                TablesPrefix = jobsSettings.JobsHangfirePrefix,
                TransactionTimeout = TimeSpan.FromSeconds(jobsSettings.TransactionTimeoutInSeconds),
                PrepareSchemaIfNecessary = jobsSettings.PrepareSchemaIfNecessary,
                DashboardJobListLimit = jobsSettings.DashboardJobListLimit,
            };

            JobStorage.Current = new MySqlStorage(jobsSettings.DbConnectionString, mysqlStorageOptions);
            GlobalConfiguration.Configuration.UseSerializerSettings(new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

            services.AddHangfire(config =>
            {
                config.UseStorage(new MySqlStorage(jobsSettings.DbConnectionString, mysqlStorageOptions));
            });

            services.AddScoped<IBackgroundJobClient>(sp => new BackgroundJobClient(JobStorage.Current));

            services.AddTransient<IForecastUpdaterService>(forSer => new ForecastUpdaterService(provider));

            return services;
        }

        public static void UseHangfire(this IApplicationBuilder app)
        {
            IJobsSettings jobsSettings = app.ApplicationServices.GetService<IJobsSettings>();
            
            GlobalConfiguration.Configuration.UseStorage(new MySqlStorage(jobsSettings.DbConnectionString, new MySqlStorageOptions
            {
                TablesPrefix = jobsSettings.JobsHangfirePrefix
            }));

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                Queues = jobsSettings.ProcessingQueues
            });

            app.UseHangfireDashboard($"/jobs", new DashboardOptions
            {
                Authorization = new[] 
                { 
                    new HangfireAuthFilter(jobsSettings.JobsServerUsername, jobsSettings.JobsServerPassword) 
                },
                DisplayStorageConnectionString = true,
                IgnoreAntiforgeryToken = true
            });
        }

        public static void AddBackgroundJobs(this IApplicationBuilder app)
        {
            TimeZoneInfo timeZone = TZConvert.GetTimeZoneInfo(_centralEuropeanTimeZoneId);

            using IServiceScope scope = app.ApplicationServices.CreateScope();

            IForecastSettings forecastSettings = scope.ServiceProvider.GetService<IForecastSettings>();
            IForecastUpdaterService forecastService = scope.ServiceProvider.GetService<IForecastUpdaterService>();

            RecurringJob.AddOrUpdate("Update forecast data",
                                    () => forecastService.UpdateForecastAsync(),
                                    forecastSettings.CronJobInterval,
                                    timeZone);
        }
    }
}
