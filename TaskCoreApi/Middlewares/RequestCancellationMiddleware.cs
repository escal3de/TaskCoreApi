namespace TaskCoreApi.Middlewares;

public class RequestCancellationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestCancellationMiddleware> _logger;

    public RequestCancellationMiddleware(RequestDelegate next, ILogger<RequestCancellationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }

        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
            _logger.LogWarning($"Request was canceled by the client ({context.Request.Method} {context.Request.Path})");
        }
    }
}

public static class RequestCancellationMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestCancellation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestCancellationMiddleware>();
    }
}