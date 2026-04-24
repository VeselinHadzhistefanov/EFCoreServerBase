using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI
{
  [Table("SofiLoveHoleItems", Schema ="")]
  public partial class SofiLoveHoleItem
  {
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string name {get; set;}

    [Required]
    public int variations {get; set;}

    [Required]
    public int num {get; set;} 
  }
}
