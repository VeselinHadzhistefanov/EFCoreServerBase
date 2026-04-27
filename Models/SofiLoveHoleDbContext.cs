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
    // server=localhost;database=sofi_love_hole;trusted_connection=false;User Id=sa;Password=PeacefulPanda1234@@@@;Persist Security Info=False;Encrypt=False
    // Server={Server Name\Instance Name};Initial Catalog={Database Name};User Id={SQL Authenticated UserName};Password={SQL Authenticated Password}
    // Server=tcp:localhost,1433;Persist Security Info=False;trusted_connection=false;User ID=sa;Password=PeacefulPanda1234@@@@;Initial Catalog=sofi_love_hole;Encrypt=False
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
      //172.17.0.2
      //faf07b1a4529
      string cs1 = "server=tcp:localhost,1433;database=sofi_love_hole;trusted_connection=false;User Id=sa;Password=PeacefulPanda1234@@@@;Persist Security Info=False;Encrypt=False";
      string cs2 = "server=172.17.0.2;Initial Catalog=sofi_love_hole;User Id=sa;Password=PeacefulPanda1234@@@@;TrustServerCertificate=True;Encrypt=true";
      string cs3 = "server=faf07b1a4529;database=sofi_love_hole;trusted_connection=false;User Id=sa;Password=PeacefulPanda1234@@@@;Persist Security Info=False;Encrypt=False";

      // string[][] cs = [];
      // cs[0] = ["server=tcp:localhost,1433", "server=localhost,1433", "server=localhost", "server=172.17.0.2", "server=172.17.0.2,1433"];
      // cs[1] = ["database=sofi_love_hole", "Initial Catalog=sofi_love_hole"];
      // cs[2] = ["User Id=sa", "User ID=sa"];
      // cs[3] = ["Password=Peacefulpanda1234@@@@"];
      // cs[4] = ["trusted_connection=false", ""];
      // cs[5] = ["Persist Security Info=False",""];
      // cs[6] = ["Encrypt=False",""];

      cs1 = "server=tcp:localhost,1433;database=sofi_love_hole;trusted_connection=false;User Id=sa;Password=PeacefulPanda1234@@@@;Persist Security Info=False;Encrypt=False";
 
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
