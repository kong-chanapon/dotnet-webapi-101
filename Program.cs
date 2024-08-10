using Microsoft.EntityFrameworkCore;
using dotnet_api.Models;
using DotNetEnv;
using dotnet_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Retrieve the connection string components from environment variables
var host = Environment.GetEnvironmentVariable("DB_HOST");
var port = Environment.GetEnvironmentVariable("DB_PORT");
var name = Environment.GetEnvironmentVariable("DB_NAME");
var username = Environment.GetEnvironmentVariable("DB_USER");
var password = Environment.GetEnvironmentVariable("DB_PASS");
var connectionString = $"Host={host};Port={port};Database={name};Username={username};Password={password}";
Console.WriteLine(connectionString);

builder.Services.AddDbContext<ItemContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<ItemService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.MapGet("/health", () => "ok");
app.UseHttpsRedirection();
app.Run();
