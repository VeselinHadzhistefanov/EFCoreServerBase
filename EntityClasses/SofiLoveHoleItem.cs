using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Identity.Client;

namespace WebAPI
{
  [Table("sofi_love_hole_items", Schema ="dbo")]
  public partial class SofiLoveHoleItem
  {
    [Required]
    [Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string item {get; set;}

    [Required]
    public int variations {get; set;}

    [Required]
    public int num {get; set;} 

    public string getDescription()
    {
      var ret = "";
      string pattern = "{0}, with {1} variations. Sofi owns {2}.";
      ret = string.Format(pattern, item, variations, num);
      return ret;
    }
  }
}
