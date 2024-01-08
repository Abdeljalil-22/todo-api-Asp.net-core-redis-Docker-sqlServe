using DemoDocker.Library.Service;
using DemoDocker.Library.Data;
using Microsoft.EntityFrameworkCore;
using Middleware;
var builder = WebApplication.CreateBuilder(args);

//builder.AddServiceDefaults();

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var server = Environment.GetEnvironmentVariable("test_App");
var port = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var rides= Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Services.AddDbContext<APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIContext") ?? throw new InvalidOperationException("Connection string 'APIContext' not found.")));

builder.Services.AddStackExchangeRedisCache(options =>
{


    options.Configuration = builder.Configuration.GetConnectionString("redis");
    });

builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddJwtAuthentication(builder.Configuration); // JWT Configuration

//builder.Configuration.AddEnvironmentVariables(Environment.NewLine");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
var app = builder.Build();

//app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>(); // JWT Middleware

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();
app.MapHealthChecks("/healthz");

app.Run();
