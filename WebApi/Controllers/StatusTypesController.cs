using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusTypesController(IStatusTypeService statusTypeService) : ControllerBase
{
    private readonly IStatusTypeService _statusTypeService = statusTypeService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _statusTypeService.GetAllAsync();
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
