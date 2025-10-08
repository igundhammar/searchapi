namespace SearchApi.Application;
public interface ISearchEngine
{
    string Name { get; }
    Task<Dictionary<string, long>> GetSearchResultsAsync(List<string> query);
}
