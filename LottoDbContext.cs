using Microsoft.EntityFrameworkCore;

public partial class LottoDbContext : DbContext
{
    public LottoDbContext(DbContextOptions dbOptions) : base(dbOptions)
    {
    }

    protected LottoDbContext()
    {
    }

    public DbSet<User> Users { get; set; }
}

