using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _userService.GetAllAsync();
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _userService.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("project-managers")]
    public async Task<IActionResult> GetProjectManagersAsync()
    {
        var result = await _userService.GetProjectManagersAsync();
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut("{id}/role/{roleId}")]
    public async Task<IActionResult> updateByIdAsync(int id, int roleId)
    {
        if(id <= 0)
        {
            return NotFound();
        }
        var result = await _userService.UpdateUserRoleAsync(id, roleId);
        if(result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}
