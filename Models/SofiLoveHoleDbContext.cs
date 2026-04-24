using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
  public partial class SofiLoveHoleDbContext : DbContext
  {
    public SofiLoveHoleDbContext(
      DbContextOptions<SofiLoveHoleDbContext> options)
        : base(options){
    }

    public virtual DbSet<SofiLoveHoleItem> SofiLoveHoleItems {get;set;}

  }
}
