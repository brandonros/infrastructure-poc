using MicroservicePOC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicePOC.Controllers;
[ApiController]
[Route("redis")]
public class RedisController : ControllerBase
{
    private readonly ILogger<PingController> _logger;
    private readonly IRedisService _redisService;

    public RedisController(ILogger<PingController> logger, IRedisService redisService)
    {
        _logger = logger;
        _redisService = redisService;
    }

    [HttpGet("keys/{keyName}")]
    public async Task<IActionResult> Get(string keyName)
    {
        _logger.LogInformation("redis");
        var value = _redisService.Get(keyName);
        return new OkObjectResult(value);
    }
}
