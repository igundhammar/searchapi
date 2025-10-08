using Microsoft.Extensions.Logging;
using SearchApi.Domain;

namespace SearchApi.Application;
public class SearchApplication : ISearchApplication
{
    private readonly ISearchService _searchService;
    private readonly ILogger<SearchApplication> _logger;

    public SearchApplication(ISearchService searchService, ILogger<SearchApplication> logger)
    {
        _searchService = searchService;
        _logger = logger;
    }

    public async Task<List<SearchResult>> SearchQueryAsync(List<string> searchWords, List<string> searchEngines)
    {
        if (searchWords == null || searchWords.Count == 0)
        {
            _logger.LogError("Search words are empty.");
            throw new ArgumentException("Search words cannot be null or empty.", nameof(searchWords));
        }
        if (searchEngines == null || searchEngines.Count == 0)
        {
            _logger.LogError("Search engines are empty.");
            throw new ArgumentException("Search engines cannot be null or empty.", nameof(searchEngines));
        }

        return await _searchService.SearchEngines(searchWords, searchEngines);
    }

}
