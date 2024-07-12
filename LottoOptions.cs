using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class LottoOptions
{
    public readonly DbContextOptions<LottoDbContext> dbOptions;

    public LottoOptions()
    {
        IConfigurationRoot configuration = GetConfiguration();
        dbOptions = GetDbOptions(configuration);        
    }

    private static IConfigurationRoot GetConfiguration()
    {
        // Access json file for configuration settings
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("AppSettings.json")
            .Build();
    }

    private static DbContextOptions<LottoDbContext> GetDbOptions(IConfigurationRoot configuration)
    {
        // Create options for accessing database
        var optionsBuilder = new DbContextOptionsBuilder<LottoDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        DbContextOptions<LottoDbContext> dbOptions = optionsBuilder.Options;
        return dbOptions;
    }
}

