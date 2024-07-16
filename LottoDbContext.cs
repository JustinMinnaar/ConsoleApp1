using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public partial class LottoDbContext : DbContext
{
    public LottoDbContext()
    {
    }
        
    public LottoDbContext(DbContextOptions<LottoDbContext> dbOptions) : base(dbOptions)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }
    
    public DbSet<User> Users { get; set; }
}

