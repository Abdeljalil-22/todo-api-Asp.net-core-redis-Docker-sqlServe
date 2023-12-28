using Microsoft.EntityFrameworkCore;
using DemoDocker.Library.Service;
using DemoDocker.Library.Data;
using Middleware;
var builder = WebApplication.CreateBuilder(args);

//builder.AddServiceDefaults();
//builder.Services.AddDbContext<DemoDockerUserApiContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoDockerUserApiContext") ?? throw new InvalidOperationException("Connection string 'DemoDockerUserApiContext' not found.")));

// Add services to the container.

builder.Services.AddDbContext<APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIContext") ?? throw new InvalidOperationException("Connection string 'APIContext' not found.")));

//builder.Services.AddStackExchangeRedisCache(options =>
//{


//    options.Configuration = builder.Configuration.GetConnectionString("redis");
//});

builder.Services.AddJwt(builder.Configuration);

//builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();


app.Run();
