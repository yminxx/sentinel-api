using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sentinel.API.Controllers;

[ApiController]
[Route("profile")]
public class ProfileController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);
        var role = User.FindFirstValue(ClaimTypes.Role);
            
        return Ok(new
        {
            success = true,
            message = "Profile retrieved successfully.",
            data = new
            {
                userId,
                email,
                role
            }
        });
    }
}