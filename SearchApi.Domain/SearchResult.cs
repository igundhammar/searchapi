namespace SearchApi.Domain;
public class SearchResult
{
    public string EngineName { get; set; } = string.Empty;
    public long TotalCount { get; set; }
    public Dictionary<string, long>? WordCounts { get; set; }
}

