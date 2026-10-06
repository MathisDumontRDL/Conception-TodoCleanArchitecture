using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Pong()
    {
        return Ok("Pong");
    }
}
