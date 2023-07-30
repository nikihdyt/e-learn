using ELearnAPI.EfCore;
using ELearnAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

// Add services to the container.
builder.Services.AddDbContext<EF_DataContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("Ef_Postgres_Db"))
    );
builder.Services.AddScoped<JwtUtils>(_ =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    var secretKey = jwtSettings["SecretKey"];
    return new JwtUtils(secretKey);
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
