using Business.Interfaces;
using Business.Models.Project;
using Data.Entities;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        if(id <= 0)
        {
            return BadRequest();
        }

        var result = await _projectService.GetProjectAsync(x => x.Id == id);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(ProjectUpdateForm form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }


        var result = await _projectService.UpdateProjectAsync(form);
        if (result == null || !result.Success)
        {
            return NotFound(result?.Message ?? "Project not found");
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var result = await _projectService.DeleteProjectAsync(x => x.Id == id);
        if (result == null || !result.Success) 
        {
            return NotFound(result?.Message ?? ""); 
        }

        return Ok(result);
    }
}
