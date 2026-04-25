using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace WebAPI
{
  public partial class SofiLoveHoleDbContext : DbContext
  {
    public SofiLoveHoleDbContext(
      DbContextOptions<SofiLoveHoleDbContext> options)
      : base(options){
    }
    // DefaultConnection": "server=localhost;database=sofi_love_hole;trusted_connection=false;User Id=sa;Password=PeacefulPanda1234@@@@;Persist Security Info=False;Encrypt=False
    // Server={Server Name\Instance Name};Initial Catalog={Database Name};User Id={SQL Authenticated UserName};Password={SQL Authenticated Password};
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
      //172.17.0.2
      string cs1 = "server=localhost;database=sofi_love_hole;trusted_connection=false;User Id=sa;Password=PeacefulPanda1234@@@@;Persist Security Info=False;Encrypt=False";
      string cs2 = "server=172.17.0.2;Initial Catalog=sofi_love_hole;User Id=sa;Password=PeacefulPanda1234@@@@;TrustServerCertificate=True;Encrypt=true";
      //cs1 = @"Server=tcp:localhost,1433;Persist Security Info=False;trusted_connection=false;User ID=sa;Password=PeacefulPanda1234@@@@;Initial Catalog=sofi_love_hole;Encrypt=False";
      
      optionsBuilder.UseSqlServer(
        cs2, builder =>
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
