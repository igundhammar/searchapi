using SearchApi.Infrastructure.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace SearchApi.Infrastructure.SearchEngines;

public class GoogleEngine : SearchEngineBase
{
    public override string Name => "Google";
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly string _cx;

    public GoogleEngine(HttpClient httpClient, ApiSettings apiSettings) : base(httpClient)
    {
        _apiKey = apiSettings.GoogleConfig.ApiKey;
        _baseUrl = apiSettings.GoogleConfig.BaseUrl;
        _cx = apiSettings.GoogleConfig.Cx;
    }

    protected override HttpRequestMessage BuildRequest(string word)
    {
        var requestUri = $"{_baseUrl}?key={_apiKey}&cx={_cx}&q={Uri.EscapeDataString(word)}";
        return new HttpRequestMessage(HttpMethod.Get, requestUri);
    }

    protected override long? ParseCount(JsonDocument json)
    {
        if (json.RootElement.TryGetProperty("searchInformation", out var searchInfo) &&
            searchInfo.TryGetProperty("totalResults", out var totalResults))
        {
            if (long.TryParse(totalResults.GetString(), out var count))
                return count;
        }
        return null;
    }
}
