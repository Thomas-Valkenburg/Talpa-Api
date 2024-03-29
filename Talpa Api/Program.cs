using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Talpa_Api.Contexts;

namespace Talpa_Api
{
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

            const string connection = "Server=tcp:cgi-the-boys.database.windows.net,1433;Initial Catalog=cgi_the_boys;Persist Security Info=False;User ID=the_boys_admin;Password=Database123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            
            Console.WriteLine($"connection: {connection}");

            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(connection, option =>
                {
                    option.EnableRetryOnFailure(2);
                }));
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}