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
                var userContext = services.GetRequiredService<UserContext>();
                DbInitializer.Initialize(userContext);
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
}
