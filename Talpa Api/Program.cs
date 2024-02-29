using Microsoft.EntityFrameworkCore;
using Talpa_Api.Context;

namespace Talpa_Api;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var suggestionContext = services.GetRequiredService<SuggestionContext>();
                var teamContext = services.GetRequiredService<TeamContext>();
                var userContext = services.GetRequiredService<UserContext>();
                var tagContext = services.GetRequiredService<TagContext>();
                
                EnsureDatabaseExists([suggestionContext, teamContext, userContext, tagContext]);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the db.");
            }
        }

        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureLogging(logger =>
        {
            logger.ClearProviders().AddSimpleConsole(conf =>
            {
                conf.TimestampFormat = "[dd/MM/yyyy HH:mm:ss] ";
            });
        }).ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
    }

    private static void EnsureDatabaseExists(List<DbContext> contexts)
    {
        contexts.ForEach(context =>
        {
            context.Database.EnsureCreated();

            context.SaveChanges();
        });
    }
}
