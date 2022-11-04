namespace PTW_API.Services.Utils.Logging
{
    using Serilog.Context;

    public static class Correlator
    {
        private static AsyncLocal<string> CorrelationId = new AsyncLocal<string>();

        public static string CurrentCorrelationId => CorrelationId.Value ?? "";

        public static IDisposable BeginCorrelationScope(string correlationId)
        {
            if (CorrelationId.Value != null)
            {
                throw new InvalidOperationException("Already is an operation.");
            }

            CorrelationId.Value = correlationId;

            return new CorrelationScope(LogContext.PushProperty("CorrelationId", correlationId));
        }

        private class CorrelationScope : IDisposable
        {
            private readonly IDisposable _logContextPop;
            
            public CorrelationScope(IDisposable logContextPop)
            {
                _logContextPop = logContextPop ?? throw new ArgumentNullException(nameof(logContextPop));
            }

            public void Dispose() => _logContextPop.Dispose();
        }
    }
}
