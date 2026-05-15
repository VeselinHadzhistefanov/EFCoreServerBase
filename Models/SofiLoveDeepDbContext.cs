
using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
  public partial class SofiLoveDeepDbContext : DbContext
  {
    public SofiLoveDeepDbContext(
      DbContextOptions<SofiLoveDeepDbContext> options)
      : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);

      string cs1 = @"
        server=localhost,1401;
        database=sofi_love_deep;
        user id=sa;
        password=PeacefulPanda1234@@@@;
        encrypt=false;
        ";

      optionsBuilder.UseSqlServer(
        cs1, builder =>
        {
          builder.EnableRetryOnFailure(5, System.TimeSpan.FromSeconds(1), null);
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<SofiLoveHoleItem> SofiLoveHoleItems { get; set; }

  }
}
