using SearchApi.Application;
using System.Text.Json;

namespace SearchApi.Infrastructure.SearchEngines;

public abstract class SearchEngineBase : ISearchEngine
{
    protected readonly HttpClient _httpClient;
    public abstract string Name { get; }

    protected SearchEngineBase(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Base implementation of GetSearchResultsAsync for common logic of engines
    // Potential use of RequestResult object here and then to map to SearchResult(DTO)
    public async Task<Dictionary<string, long>> GetSearchResultsAsync(List<string> queryWords)
    {
        var results = new Dictionary<string, long>();

        // Potential use of retry-logic, if request time out for example
        foreach (var word in queryWords)
        {
            HttpRequestMessage request = BuildRequest(word);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            using var json = JsonDocument.Parse(content);

            long? count = ParseCount(json);
            results[word] = count ?? 0;
        }

        return results;
    }

    // Engine specific methods
    protected abstract HttpRequestMessage BuildRequest(string word);

    protected abstract long? ParseCount(JsonDocument json);
}
