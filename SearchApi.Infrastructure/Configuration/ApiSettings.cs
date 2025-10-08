namespace SearchApi.Infrastructure.Configuration;
public class ApiSettings
{
    // Using configs to flexibility in data source, easy to mock, all cofigs in one place
    public GoogleConfig GoogleConfig { get; set; }
    public BingConfig BingConfig { get; set; }
}

public class GoogleConfig
{
    public string ApiKey { get; set; } = "";
    public string Cx { get; set; } = "";
    public string BaseUrl { get; set; } = "";
}

public class BingConfig
{
    public string ApiKey { get; set; } = "";
    public string BaseUrl { get; set; } = "";
}
