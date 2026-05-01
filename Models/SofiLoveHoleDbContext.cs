using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
  public partial class SofiLoveHoleDbContext : DbContext
  {
    public SofiLoveHoleDbContext(
      DbContextOptions<SofiLoveHoleDbContext> options)
      : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);

      string cs1 = @"
        server=localhost;
        database=sofi_love_hole;
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
