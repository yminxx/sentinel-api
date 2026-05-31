using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sentinel.Application.Abstractions.Persistence;
using Sentinel.Domain.Entities;

namespace Sentinel.API.Controllers;

[ApiController]
[Route("admin")]
public class AdminController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IAuditLogRepository _auditLogRepository;

    public AdminController(IUserRepository userRepository, IAuditLogRepository auditLogRepository)
    {
        _userRepository = userRepository;
        _auditLogRepository = auditLogRepository;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        return Ok(new { success = true, message = "Welcome Admin." });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userRepository.GetAllAsync();

        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"{claim.Type}: {claim.Value}");
        }

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        return Ok(
            new
            {
                success = true,
                message = "Users retrieved successfully.",
                data = users.Select(x => new
                {
                    x.Id,
                    x.Email,
                    x.Role,
                }),
            }
        );
    }
}
