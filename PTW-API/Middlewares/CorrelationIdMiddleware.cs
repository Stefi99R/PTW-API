namespace PTW_API.Middlewares
{
    using PTW_API.Contracts;

    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
        {
            using (Correlator.BeginCorrelationScope(correlationIdGenerator.Get()))
            {
                await _next.Invoke(context);
            }
        }
    }
}
