using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using SearchApi.Domain;

namespace SearchApi.Tests;
public class SearchServiceTests
{
    private readonly Mock<ISearchService> _searchServiceMock;
    private readonly Mock<ILogger<SearchService>> _loggerMock;

    public SearchServiceTests()
    {
        _searchServiceMock = new Mock<ISearchService>();
        _loggerMock = new Mock<ILogger<SearchService>>();
    }

    [Fact]
    public async Task GetTotalResultsAsync_Returns_Correct_Result()
    {
        // Arrange  
        var mockResult = new List<SearchResult>();
        var mock = new SearchResult
        {
            EngineName = "Google",
            TotalCount = 1,
            WordCounts = new Dictionary<string, long> { { "test1", 1 } }
        };
        mockResult.Add(mock);
        _searchServiceMock.Setup(s => s.SearchEngines(It.IsAny<List<string>>(), It.IsAny<List<string>>())).ReturnsAsync(mockResult);

        var service = _searchServiceMock.Object;

        // Act  
        var result = await service.SearchEngines(new List<string> { "test1" }, new List<string> { "Google" });

        // Assert  
        Assert.Equal("Google", result[0].EngineName);
        Assert.Equal(1, result[0].TotalCount);
        Assert.Equal(1, result[0].WordCounts["test1"]);
    }

    [Fact]
    public async Task GetTotalResultsAsync_With_Empty_Query_Returns_Correct_Result()
    {
        // Arrange  
        var mockResult = new List<SearchResult>();
        var mock = new SearchResult
        {
        };
        mockResult.Add(mock);
        _searchServiceMock.Setup(s => s.SearchEngines(It.Is<List<string>>(q => q.Count == 0), It.Is<List<string>>(q => q.Count == 0))).ReturnsAsync(mockResult);

        var service = _searchServiceMock.Object;

        // Act  
        var result = await service.SearchEngines(new List<string>(), new List<string>());

        // Assert  
        Assert.Empty(result[0].EngineName);
        Assert.Equal(0, result[0].TotalCount);
        Assert.Null(result[0].WordCounts);
    }

    [Fact]
    public async Task GetTotalResultsAsync_With_Multiple_Queries_Returns_Correct_Result()
    {
        // Arrange  
        var mockResult = new List<SearchResult>();
        var mock1 = new SearchResult
        {
            EngineName = "Google",
            TotalCount = 2,
            WordCounts = new Dictionary<string, long> { { "test2", 2 } }
        };

        var mock2 = new SearchResult
        {
            EngineName = "Bing",
            TotalCount = 3,
            WordCounts = new Dictionary<string, long> { { "test3", 3 } }
        };

        mockResult.Add(mock1);
        mockResult.Add(mock2);

        _searchServiceMock.Setup(s => s.SearchEngines(It.Is<List<string>>(q => q.Count == 2), It.IsAny<List<string>>()))
            .ReturnsAsync(mockResult);

        var service = _searchServiceMock.Object;

        // Act  
        var result = await service.SearchEngines(new List<string> { "test2", "test3" }, new List<string> { "Google", "Bing" });

        // Assert  
        Assert.Equal("Google", result[0].EngineName);
        Assert.Equal(2, result[0].TotalCount);
        Assert.Equal(2, result[0].WordCounts["test2"]);

        Assert.Equal("Bing", result[1].EngineName);
        Assert.Equal(3, result[1].TotalCount);
        Assert.Equal(3, result[1].WordCounts["test3"]);
    }

    [Fact]
    public async Task SearchEngines_Returns_Correct_Results_With_Custom_Mock()
    {
        // Arrange  
        var engine1 = new SearchEngineMock { Name = "Google", ResultCount = 4 };
        var engine2 = new SearchEngineMock { Name = "Bing", ResultCount = 5 };
        var service = new SearchService(new List<ISearchEngine> { engine1, engine2 }, _loggerMock.Object);

        // Act  
        var result = await service.SearchEngines(new List<string> { "test" }, new List<string> { "Google", "Bing" });

        // Assert  
        Assert.Contains(result, r => r.EngineName == "Google" && r.TotalCount == 4 && r.WordCounts["test"] == 4);
        Assert.Contains(result, r => r.EngineName == "Bing" && r.TotalCount == 5 && r.WordCounts["test"] == 5);
    }
}
