var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomRateLimiter();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddSingleton<UserStorage>();
builder.Services.AddSingleton<ProjectStorage>();
builder.Services.AddSingleton<TaskCounterStorage>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRateLimiter();
app.UseRequestCancellation(); // - кастомный логгер, находиться в директории "middlewares"
app.UseLoggingRequests(); // - кастомный логгер, находиться в директории "middlewares"
app.UseRouting();
//app.UseHttpsRedirection();
app.MapControllers();

app.Run();