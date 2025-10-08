namespace SearchApi.Tests;
public class SearchEngineMock : ISearchEngine
{
    public string Name { get; set; }
    public int ResultCount { get; set; }

    public Task<Dictionary<string, long>> GetSearchResultsAsync(List<string> query)
    {
        var results = query.ToDictionary(q => q, q => (long)ResultCount);
        return Task.FromResult(results);
    }
}
