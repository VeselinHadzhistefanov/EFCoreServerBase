using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace WebAPI.Controllers
{

  [Route("{ENTITY}_item_sets")]
  [ApiController]
  public class ItemSetController<ENTITY> : BaseApiController
  {
    public ItemSetController(DbContext context, ILogger<BaseApiController> logger) : base(logger)
    {
      _DbContext = context;
      _logger = logger;
      _logger.LogError(typeof(ENTITY).ToString()); 
    }

    private DbContext _DbContext;
    private ILogger _logger;
    //private const string ENTITY = "sofi_love_hole_items";


    [HttpGet]
    public IActionResult Get()
    {
      IActionResult? ret = null;
      List<string> list = new List<string>();

      // try
      // {
      //   DbSet<SofiLoveHoleItem> tableEntries = _DbContext.SofiLoveHoleItems;
      //   int count = tableEntries.Count();

      //   if (count > 0)
      //   {
      //     string result = "Number of items Sofi owns: " + count;

      //     // Sort keyless entries in alphabetical order
      //     list = _DbContext.SofiLoveHoleItems.OrderBy(v => string.Compare("0", v.item)).ToList();

      //     for (int i = 0; i < count; i++)
      //     {
      //       var item = list[i];
      //       result += "\nNumber " + (i + 1) + " item: ";
      //       //result += getEntryString(item);
      //       //result += item.getDescription();
      //     }

      //     ret = StatusCode(StatusCodes.Status200OK, result);
      //   }
      //   else
      //   {
      //     ret = StatusCode(StatusCodes.Status404NotFound, "Unable to retrieve values of " + ENTITY_NAME + " in the system");
      //   }
      // }
      // catch (Exception e)
      // {
      //   ret = HandleException(e, "Exception trying to get all " + ENTITY_NAME + " values.");
      // }

      return ret;
    }
  }
}