using System.Text.Json;

namespace APICallerLibrary;

public class APIController : IAPIController
{
    private readonly HttpClient client = new();

    public async Task<string> CallAPIAsync(string url, string content, HttpMethods method = HttpMethods.GET, bool formatOutput = true)
    {
        StringContent stringContent = new(content, System.Text.Encoding.UTF8, "application/json");
        return await CallAPIAsync(url, stringContent, method, formatOutput);
    }
    public async Task<string> CallAPIAsync(string url, HttpContent? content = null, HttpMethods method = HttpMethods.GET, bool formatOutput = true)
    {
        HttpResponseMessage? response;

        switch (method)
        {
            case HttpMethods.GET:
                response = await client.GetAsync(url);
                break;
            case HttpMethods.POST:
                response = await client.PostAsync(url, content);
                break;
            case HttpMethods.PATCH:
                response = await client.PatchAsync(url, content);
                break;
            case HttpMethods.PUT:
                response = await client.PutAsync(url, content);
                break;
            case HttpMethods.DELETE:
                response = await client.DeleteAsync(url);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(method), method, null);
        }

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
            string prettyJson = JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions { WriteIndented = true });
            return prettyJson;
        }
        else
        {
            return $"Error: {response.StatusCode}";
        }
    }

    public bool IsValidUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;

        bool isValid = Uri.TryCreate(url, UriKind.Absolute, out Uri uriOutput) &&
            ((uriOutput.Scheme == Uri.UriSchemeHttps) || (uriOutput.Scheme == Uri.UriSchemeHttp));
        return isValid;
    }
}
