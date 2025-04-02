using Microsoft.AspNetCore.Mvc;
using WebAPIAdvertisingPlatform.Services;

namespace WebAPIAdvertisingPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
[Route("api")]
public class AdvertisingPlatformController : ControllerBase
{
    private readonly ILogger<AdvertisingPlatformController> _logger;
    private readonly IRegionService _regionService;

    public AdvertisingPlatformController(ILogger<AdvertisingPlatformController> logger, IRegionService regionService)
    {
        _regionService = regionService;
        _logger = logger;
    }


    [HttpPost]
    public IActionResult LoadPlatforms([FromForm] IFormFile file)
    {
        _regionService.LoadPlatforms(file);
        return Ok();
    }

    [HttpGet]
    public IActionResult GetPlatforms([FromQuery] string location)
    {
        var result = _regionService.FindPlatforms(location);
        if(result == null)
            return NotFound(location);
        return Ok(result);
    }
}
