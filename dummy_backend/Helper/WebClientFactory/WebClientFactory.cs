using System;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Microsoft.Net.Http.Headers;
using TerraCottaV2.Helpers;

namespace dummy_backend.Helper.WebClientFactory
{
    /// <summary>
    /// Provides http clients for requesting classes
    /// </summary>
    public class WebClientFactory : IWebClientFactory
    {
        // There is some detailed information about using HttpClient in dotnet:
        // https://josef.codes/you-are-probably-still-using-httpclient-wrong-and-it-is-destabilizing-your-software/

        private readonly IHttpClientFactory _clientFactory;

        private static string _userAgent;

        /// <summary>
        /// Creates new instance
        /// </summary>
        public WebClientFactory(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// Get the http client for the calling class
        /// </summary>
        /// <param name="caller">The calling class. Usually "this".</param>
        /// <returns></returns>
        public HttpClient GetClient(object caller)
        {
            var callingClass = caller.GetType();

            var client = _clientFactory.CreateClient(callingClass.Name);
            if (client.DefaultRequestHeaders.Contains(HeaderNames.UserAgent))
                return client;

            // Set default user agent
            client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, GetDefaultUserAgent());
            return client;
        }

        /// <summary>
        /// Create a default user-agent based on current assembly and version
        /// </summary>
        /// <returns>String in format "ApplicationName/1.0 (Microsoft Windows 10; SERVER-01; .NET Core 3.1)"</returns>
        private static string GetDefaultUserAgent()
        {
            if (_userAgent != null)
                return _userAgent;
            var sb = new StringBuilder();

            var projectName = Assembly.GetEntryAssembly()?.GetName().Name;

            if (projectName != null)
            {
                sb.Append(projectName);
            }

            sb.Append(Assembly.GetExecutingAssembly().GetName().Name);

            sb.Append(" (");
            sb.Append(Environment.MachineName);
            sb.Append(")");

            return _userAgent = sb.ToString();
        }
    }
}
