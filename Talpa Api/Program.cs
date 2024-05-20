using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Talpa_Api.Contexts;

namespace Talpa_Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(option =>
        {
            option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
            
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(option =>
        {
            option.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });

        builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
        var connection = builder.Configuration.GetConnectionString("ConnectionString") 
                         ?? throw new Exception("Connection string not found.");
            
        builder.Services.AddDbContext<Context>(options =>
            options.UseMySQL(connection, option =>
            {
                option.EnableRetryOnFailure(2);
            })
        );

        var app = builder.Build();

        // Migrate Database to latest version
        using (var scope = app.Services.CreateScope())
        {
	        var context = scope.ServiceProvider.GetRequiredService<Context>();
	        context.Database.Migrate();
        }

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

		app.UseStaticFiles();

        app.UseCors("AllowAll");

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}