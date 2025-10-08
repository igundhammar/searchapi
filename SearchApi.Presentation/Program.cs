using Microsoft.Extensions.Options;
using SearchApi.Application;
using SearchApi.Infrastructure.Configuration;
using SearchApi.Infrastructure.SearchEngines;
using SearchApi.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Api settings config
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<ApiSettings>>().Value);

// Engines
builder.Services.AddHttpClient<ISearchEngine, GoogleEngine>();
builder.Services.AddHttpClient<ISearchEngine, BingEngine>();

// Services
builder.Services.AddScoped<ISearchService, SearchService>();

// Application
builder.Services.AddScoped<ISearchApplication, SearchApplication>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
