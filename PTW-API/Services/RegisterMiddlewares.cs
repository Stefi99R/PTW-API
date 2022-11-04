using PTW_API.Middlewares;

namespace PTW_API.Services
{
    public static class RegisterMiddlewares
    {
        public static IApplicationBuilder AddPTWMiddlewares(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();

            return applicationBuilder;
        }
    }
}
