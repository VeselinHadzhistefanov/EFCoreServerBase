using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;
using System.Diagnostics.Eventing.Reader;

namespace WebAPI.Controllers
{
  public class BaseApiController : ControllerBase
  {
    private ILogger<BaseApiController> _logger;
    public BaseApiController(ILogger<BaseApiController> logger)
    {
      _logger = logger;
    }

    protected IActionResult HandleException(Exception ex,
      string msg)
    {
      IActionResult ret;

      // TODO: Publish exceptions here
      msg += " Exception error: ```" + ex.Message + "```";
      _logger.LogError(msg);
      // Create new exception with generic message        
      ret = StatusCode(StatusCodes.Status500InternalServerError,
              new Exception(msg));

      return ret;
    }
    public void HandleLogData(object data)
    {
      try
      {
        string s = data.ToString()!;
        _logger.LogInformation(s);
      }
      catch (Exception e)
      {
        _logger.LogError(e.Message);
      }
    }
  }
}
