using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Byhands.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet("private")]
    [Authorize]
    public async Task<IActionResult> Private()
    {
        var currentUser = GetCurrentUser();

        if (currentUser == null)
            return BadRequest();

        return Ok($"Hi, {currentUser}");
    }

    [HttpGet("Public")]
    public IActionResult Public()
    {
        return Ok("Hi, you're on public property");
    }

    private string? GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity != null)
        {
            var userClaims = identity.Claims;

            return userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value;
        }

        return null;
    }
}