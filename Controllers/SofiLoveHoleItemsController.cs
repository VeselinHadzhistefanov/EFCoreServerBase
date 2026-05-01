using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace WebAPI.Controllers
{
  [Route("sofi_love_hole")]
  [ApiController]
  public class SofiLoveHoleItemsController : BaseApiController
  {
    private static Dictionary<string, string> SofiLoveHoleItemsPattern = new Dictionary<string, string>
    {
      {"item", "{0}"},
        {"variations", " with {0} variations"},
        {"num", ", Sofi owns {0}."}
    };
    public SofiLoveHoleItemsController(SofiLoveHoleDbContext context) : base()
    {
      _DbContext = context;
    }

    private SofiLoveHoleDbContext _DbContext;

    private const string ENTITY_NAME = "sofi_love_hole_items";

    [HttpGet]
    public IActionResult Get()
    {
      IActionResult? ret = null;
      List<SofiLoveHoleItem> list = new List<SofiLoveHoleItem>();

      try
      {
        DbSet<SofiLoveHoleItem> tableEntries = _DbContext.SofiLoveHoleItems;
        int count = tableEntries.Count();

        if (count > 0)
        {
          string result = "Number of items Sofi owns: " + count;

          // Sort keyless entries in alphabetical order
          list = _DbContext.SofiLoveHoleItems.OrderBy(v => string.Compare("0", v.item)).ToList();

          for (int i = 0; i < count; i++)
          {
            var item = list[i];
            result += "\nNumber " + (i + 1) + " item: ";
            result += getEntryString(item);
            //result += item.getDescription();
          }

          ret = StatusCode(StatusCodes.Status200OK, result);
        }
        else
        {
          ret = StatusCode(StatusCodes.Status404NotFound, "Unable to retrieve values of " + ENTITY_NAME + " in the system");
        }
      }
      catch (Exception e)
      {
        ret = HandleException(e, "Exception trying to get all " + ENTITY_NAME + " values.");
      }

      return ret;
    }

    private string getEntryString<T>(T entry)
    {
      var ret = "";
      var properties = entry.GetType().GetProperties();
      Dictionary<string, string> pattern_dict ;

      if (typeof(T).Equals(typeof(SofiLoveHoleItem))){
        pattern_dict = new Dictionary<string, string>{{"item", "{0}"}, {"variations", ", with {0} variations. "}, {"num", "Sofi owns {0}."}};
      }
      else {
        return entry == null ? "" : entry.ToString()!;
      }

      foreach (var property in properties)
      {
        var name = property.Name;
        var val = property.GetValue(entry);
        var pattern = pattern_dict[name];
        var label = string.Format(pattern, val);

        ret += label;
      }

      return ret;
    }
  }
}