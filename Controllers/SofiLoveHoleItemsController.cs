using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("sofi_love_hole")]
    [ApiController]

    public class SofiLoveHoleItemsController : BaseApiController
    {
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
                if (_DbContext.SofiLoveHoleItems.Count() > 0)
                {
                    int itemCount = _DbContext.SofiLoveHoleItems.Count();
                    string result = "Number of items Sofi owns: " + _DbContext.SofiLoveHoleItems.Count().ToString() + "\n";
                    //list = _DbContext.SofiLoveHoleItems.OrderBy(v => v.item).ToList();
                    list = _DbContext.SofiLoveHoleItems.OrderBy(v => string.Compare("0", v.item)).ToList();

                    // Type t = list[0].GetType();
                    // var fields = t.GetFields();

                    for (int i = 0; i < itemCount; i++)
                    {
                        var item = list[i];
                        result += "Number " + i + 
                            " item: " + item.item + 
                            ", Sofi owns " + item.num + 
                            ". Item has " + 
                            item.variations + "+ variations. \n";
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
    }
}