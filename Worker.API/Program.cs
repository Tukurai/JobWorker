
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Worker.Infrastructure.Data;

namespace Worker.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string? connString = builder.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connString))
            throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        // Add services to the container.
        builder.Services.AddDbContext<WorkerDbContext>(options => options.UseSqlite(connString));

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

        var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? [];
        if (allowedOrigins.Length == 0)
            throw new InvalidOperationException("AllowedOrigins configuration is missing or empty.");

        builder.Services.AddCors(options => 
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options => options.Servers = [new ScalarServer("http://localhost:5000")]); // http://localhost:5000/scalar/
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCors();

        app.MapControllers();

        app.Run();
    }
}
