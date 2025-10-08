using Microsoft.AspNetCore.Mvc;
using SearchApi.Domain;
using SearchApi.Application;

namespace SearchApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchApplication _searchApplication;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ISearchApplication searchApplication, ILogger<SearchController> logger)
        {
            _searchApplication = searchApplication;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string query, [FromQuery] List<string> searchEngines)
        {
            // For demo purposes, set searchEngines in the SearchQuery constructor, in case you would want to expand functionality (for example validation)
            SearchQuery searchQuery = new(query, searchEngines);
            List<string> searchWords = searchQuery.Words;

            try
            {
                List<SearchResult> results = await _searchApplication.SearchQueryAsync(searchWords, searchEngines);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the search query.");
                return StatusCode(500, new { Message = "An error occurred while processing your request. Please try again later." });
            }
        }
    }
}
