using SearchApi.Domain;

namespace SearchApi.Application;
public interface ISearchService
{
    Task<List<SearchResult>> SearchEngines(List<string> searchWords, List<string> searchEngines);
}
