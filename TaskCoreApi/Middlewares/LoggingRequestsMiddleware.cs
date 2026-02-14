namespace TaskCoreApi.Middlewares;

public class LoggingRequestsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingRequestsMiddleware> _logger;

    public LoggingRequestsMiddleware(RequestDelegate next, ILogger<LoggingRequestsMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        string requestMethod = httpContext.Request.Method;
        string requestPath = httpContext.Request.Path;
        
        Stopwatch stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(httpContext);
        }

        finally
        {
            stopwatch.Stop();
            
            int statusCode = httpContext.Response.StatusCode;
            long elapsedTime = stopwatch.ElapsedMilliseconds;
            
            _logger.LogInformation($"Request {requestMethod} {requestPath} took {elapsedTime}ms with status code {statusCode}");
            
            if (elapsedTime > 500) 
                _logger.LogWarning($"This request so slow.");
        }
    }
}

public static class LoggingRequestsMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingRequests(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingRequestsMiddleware>();
    }
}