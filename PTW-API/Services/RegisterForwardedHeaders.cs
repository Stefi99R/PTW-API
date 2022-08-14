namespace PTW_API.Services
{
    using Microsoft.AspNetCore.HttpOverrides;

    public static class RegisterForwardedHeaders
    {
        public static IServiceCollection AddForwardedHeaders(this IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            return services;
        } 
    }
}
