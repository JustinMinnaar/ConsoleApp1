using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using IServiceScope scope = host.Services.CreateScope();
        var l = new LottoProgram(scope.ServiceProvider);
        l.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("AppSettings.json");
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Add configuration for dependency injection
                services.Configure<LottoOptions>(hostContext.Configuration.GetSection("LottoOptions"));
                services.AddScoped<UserRepository>();
                services.AddScoped<LottoTester>();
                services.AddSingleton<LottoProgram>();

                // Add PostgreSQL database initialization
                services.AddDbContext<LottoDbContext>(options =>
                    options.UseNpgsql(hostContext.Configuration.GetConnectionString("DefaultConnection")));
            }
            // .ConfigureLogging((context, cfg) =>
            //     {
            //         cfg.ClearProviders();
            //         cfg.AddConfiguration(context.Configuration.GetSection("Logging"));
            //         cfg.AddConsole();
            //     })
                );
    }
}

public interface IMyService
{
    Task MyServiceMethod();
}

public class MyService : IMyService
{
    private readonly string _baseUrl;
    private readonly string _token;
    private readonly ILogger<MyService> _logger;

    public MyService(ILoggerFactory loggerFactory, IConfigurationRoot config)
    {
        var baseUrl = config["SomeConfigItem:BaseUrl"];
        var token = config["SomeConfigItem:Token"];

        _baseUrl = baseUrl;
        _token = token;
        _logger = loggerFactory.CreateLogger<MyService>();
    }

    public async Task MyServiceMethod()
    {
        _logger.LogDebug(_baseUrl);
        _logger.LogDebug(_token);
    }
}