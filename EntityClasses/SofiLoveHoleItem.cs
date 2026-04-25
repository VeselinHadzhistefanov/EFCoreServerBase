using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI
{
  [Table("sofi_love_hole_items", Schema ="dbo")]
  public partial class SofiLoveHoleItem
  {
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? item {get; set;}

    [Required]
    public int variations {get; set;}

    [Required]
    public int num {get; set;} 
  }
}
