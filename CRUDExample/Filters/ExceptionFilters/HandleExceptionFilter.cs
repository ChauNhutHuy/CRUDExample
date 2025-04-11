using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ExceptionFilters
{
    public class HandleExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HandleExceptionFilter> _logger;
        private readonly IHostEnvironment _hostEviroment;
        public HandleExceptionFilter(ILogger<HandleExceptionFilter> logger, IHostEnvironment hostEnvironment)
        {
            _logger = logger;   
            _hostEviroment = hostEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError("Exception filter {FilterName}.{MethodName} {ExceptionType} {ExceptionMessage}", nameof(HandleExceptionFilter), nameof(OnException), context.Exception.GetType().ToString(), context.Exception.Message);
            if (_hostEviroment.IsDevelopment())
            {
                context.Result = new ContentResult()
                {
                    Content = context.Exception.Message,
                    StatusCode = 500
                };
            }         
        }
    }
}
