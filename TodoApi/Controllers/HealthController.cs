using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("")]
public class HealthController : ControllerBase
{
    // GET /healthz
    
    [HttpGet("healthz")]
    [HttpHead("healthz")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Healthz() => Ok(new { status = "ok" });

    // GET /health (alias)
    [HttpGet("health")]
    [HttpHead("health")]
    public IActionResult Health() => Ok(new { status = "ok" });
}