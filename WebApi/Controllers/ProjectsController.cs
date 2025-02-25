using Business.Interfaces;
using Business.Models.Project;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    private readonly IProjectService _projectService = projectService;

    [HttpPost]
    public async Task<IActionResult> Create(ProjectRegistrationForm form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _projectService.AddAsync(form);
        switch (result.StatusCode)
        {
            case 201:
                return Created("", null);
            case 400:
                return BadRequest(result.Message);
            case 409:
                return Conflict(result.Message);
            default:
                return Problem(result.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _projectService.GetAllProjectsAsync();
        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}
