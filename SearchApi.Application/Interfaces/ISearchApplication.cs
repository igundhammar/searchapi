using SearchApi.Domain;

namespace SearchApi.Application;
public interface ISearchApplication
{
    Task<List<SearchResult>> SearchQueryAsync(List<string> searchWords, List<string> searchEngines);

}
