#region
using Microsoft.AspNetCore.Mvc;
#endregion

namespace HackathonExperiment.Api;

[ApiController]
[Route("_")]
public class CloudRuntimeController : ControllerBase
{
    [HttpGet("health")]
    public IActionResult HealthCheck() => this.Ok();

    [HttpGet("metrics")]
    public IActionResult Metrics() => this.Ok();

    [HttpGet("environment/{name}")]
    public IActionResult GetEnvironmentVariable(string name)
    {
        var value = Environment.GetEnvironmentVariable(name);
        return value is null ? this.NotFound() : this.Ok(value);
    }
}