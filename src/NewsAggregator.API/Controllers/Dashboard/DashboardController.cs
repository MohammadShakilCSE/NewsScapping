using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Application.UseCases;

namespace NewsAggregator.API.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class DashboardController : BaseApiController<DashboardController>
    {
        protected readonly ILogger<DashboardController> _logger;
        protected readonly ILifetimeScope _scope;
        protected readonly IFetchNewsUseCase _fetchNewsUseCase;

        public DashboardController(ILogger<DashboardController> logger, ILifetimeScope scope, IFetchNewsUseCase fetchNewsUseCase)
           : base(logger, scope)
        {
            _fetchNewsUseCase = fetchNewsUseCase;
        }

        [HttpGet]
        [Route("AllLatestNews")]
        public async Task<IActionResult> GetAllLatestNewsAsync()
        {
            try
            {
                var articles = await _fetchNewsUseCase.ExecuteAsync();
                return JsonSuccess(articles, "Fetched successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch news");
                return JsonError("Something went wrong", 500);
            }
        }
    }
}
