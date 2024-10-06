

namespace APICallerLibrary
{
    public interface IAPIController
    {
        Task<string> CallAPIAsync(string url, string content, HttpMethods method = HttpMethods.GET, bool formatOutput = true);
        Task<string> CallAPIAsync(string url, HttpContent? content = null, HttpMethods method = HttpMethods.GET, bool formatOutput = true);
        bool IsValidUrl(string url);
    }
}