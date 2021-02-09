using System.Net.Http;

namespace TerraCottaV2.Helpers
{
    /// <summary>
    /// Provides http clients for requesting classes
    /// </summary>
    public interface IWebClientFactory
    {
        /// <summary>
        /// Get the http client for the calling class
        /// </summary>
        /// <param name="caller">The calling class. Usually "this".</param>
        /// <returns></returns>
        HttpClient GetClient(object caller);
    }
}
