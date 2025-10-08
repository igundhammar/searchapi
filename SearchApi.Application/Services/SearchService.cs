using Microsoft.Extensions.Logging;
using SearchApi.Application;
using SearchApi.Domain;

namespace SearchApi.Infrastructure.Services;
public class SearchService : ISearchService
{
    private readonly IEnumerable<ISearchEngine> _engines;
    private readonly ILogger<SearchService> _logger;

    public SearchService(IEnumerable<ISearchEngine> engines, ILogger<SearchService> logger)
    {
        _engines = engines;
        _logger = logger;
    }

    public async Task<List<SearchResult>> SearchEngines(List<string> searchWords, List<string> searchEngines)
    {
        List<SearchResult> searchResults = [];

        // Only search in the specified engines
        IEnumerable<ISearchEngine> enginesToUse = _engines.Where(e => searchEngines.Contains(e.Name, StringComparer.OrdinalIgnoreCase));
        _logger.LogInformation("Using engines: {EngineNames}", string.Join(", ", enginesToUse.Select(e => e.Name)));

        // Run parallell search for faster results

        IEnumerable<Task<SearchResult>> tasks = enginesToUse.Select(async engine =>
        {
            var engineResult = await engine.GetSearchResultsAsync(searchWords);
            long totalCount = engineResult.Values.Sum();

            _logger.LogInformation("Engine: {EngineName}, Total Count: {TotalCount}", engine.Name, totalCount);
            return MapToSearchResult(engineResult, totalCount, engine.Name);
        });

        searchResults = (await Task.WhenAll(tasks)).ToList();
        return searchResults;
    }

    // Can also be placed in mapper class
    private static SearchResult MapToSearchResult(Dictionary<string, long> engineResult, long totalCount, string engineName)
    {
        return new SearchResult
        {
            EngineName = engineName,
            TotalCount = totalCount,
            WordCounts = engineResult
        };
    }
}
