namespace SearchApi.Domain;
public class SearchQuery
{
    public List<string> Words { get; set; } = [];
    public List<string> Engines { get; set; } = [];

    public SearchQuery(string query, List<string> engines)
    {
        // Make sure to split incoming query into separate words in order to search for each
        Words = [.. query.Split(' ', StringSplitOptions.RemoveEmptyEntries)];
        Engines = engines;
    }
}
