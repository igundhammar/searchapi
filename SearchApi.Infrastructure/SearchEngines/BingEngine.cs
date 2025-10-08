using SearchApi.Infrastructure.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace SearchApi.Infrastructure.SearchEngines;

public class BingEngine : SearchEngineBase
{
    public override string Name => "Bing";
    private readonly string _apiKey;
    private readonly string _baseUrl;

    public BingEngine(HttpClient httpClient, ApiSettings settings) : base(httpClient)
    {
        _apiKey = settings.BingConfig.ApiKey;
        _baseUrl = settings.BingConfig.BaseUrl;
    }

    protected override HttpRequestMessage BuildRequest(string word)
    {
        var requestUri = $"{_baseUrl}&q={Uri.EscapeDataString(word)}&api_key={_apiKey}";
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Add("Ocp-Apim-Subscription-Key", _apiKey);
        return request;
    }

    protected override long? ParseCount(JsonDocument json)
    {
        if (json.RootElement.TryGetProperty("search_information", out var webPages) &&
            webPages.TryGetProperty("total_results", out var totalMatches))
        {
            if (totalMatches.TryGetInt64(out var count))
                return count;
        }
        return null;
    }
}
