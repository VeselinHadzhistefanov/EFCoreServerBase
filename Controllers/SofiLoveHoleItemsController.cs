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
                if(_DbContext.SofiLoveHoleItems.Count() > 0)
                {
                    list = _DbContext.SofiLoveHoleItems.OrderBy(v => v.item).ToList();
                    ret = StatusCode(StatusCodes.Status200OK, ret);
                } else
                {
                    ret = StatusCode(StatusCodes.Status404NotFound, "Unable to retrieve values of " + ENTITY_NAME + " in the system");
                }
            } catch (Exception e)
            {
                ret = HandleException(e, "Exception trying to get all " + ENTITY_NAME + " values.");
            }

            return ret;
        }
    }
}