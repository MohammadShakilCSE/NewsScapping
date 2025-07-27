using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.API.Models;

namespace NewsAggregator.API.Controllers
{
    public abstract class BaseApiController<T> : ControllerBase where T : ControllerBase
    {
        protected readonly ILogger<T> _logger;
        protected readonly ILifetimeScope _scope;

        protected BaseApiController(ILogger<T> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        protected IActionResult JsonSuccess<TData>(TData data, string? message = null, int statusCode = 200)
        {
            var response = new JsonResponse<TData>(true, data, message);
            return StatusCode(statusCode, response);
        }

        protected IActionResult JsonError(string message, int statusCode = 400)
        {
            var response = new JsonResponse<object>(false, null, message);
            return StatusCode(statusCode, response);
        }
    }
}
