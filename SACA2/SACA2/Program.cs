using Microsoft.EntityFrameworkCore;
using SACA2.Data;
using SACA2.Models;
using SACA2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<FixtureGenerationService>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.MapControllers();


app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

app.Run();
