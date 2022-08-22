namespace PTW_API.Filters
{
    using Hangfire.Annotations;
    using Hangfire.Dashboard;
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System;

    public class HangfireAuthFilter : IDashboardAuthorizationFilter
    {
        private readonly string _userName;
        private readonly string _password;

        public HangfireAuthFilter(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public bool Authorize([NotNull] DashboardContext context)
        {
            HttpContext httpContext = context.GetHttpContext();

            string authHeader = httpContext.Request.Headers["Authorization"];

            if (authHeader?.StartsWith("Basic ") == true)
            {
                // Get the encoded username and password
                string encodedUsernamePassword = authHeader.Split(separator: new[] { ' ' }, count: 2, options: StringSplitOptions.RemoveEmptyEntries).LastOrDefault()?.Trim();

                // Decode from Base64 to string
                string decodedUsernamePassword = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                // Split username and password
                string username = decodedUsernamePassword.Split(separator: new[] { ':' }, count: 2).FirstOrDefault();
                string password = decodedUsernamePassword.Split(separator: new[] { ':' }, count: 2).LastOrDefault();

                // Check if login is correct
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    return _userName == username && _password == password;
                }
            }

            // Return authentication type (causes browser to show login dialog)
            httpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=Hangfire Dashboard";
            httpContext.Response.StatusCode = 401;
            context.Response.StatusCode = 401;

            return false;
        }
    }
}
