namespace PTW_API.Services
{
    using HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;

    public static class RegisterHealthChecks
    {
        public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            })
            .UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });

            return appBuilder;
        }
    }
}
