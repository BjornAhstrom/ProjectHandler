using Business.Factories;
using Business.Interfaces;
using Business.Models.Project;
using Business.Models.Response;
using Data.Entities;
using Data.Interfaces;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<ResponseResult> AddAsync(ProjectRegistrationForm form)
    {
        try
        {
            var entity = ProjectFactory.Create(form);
            if (entity == null)
            {
                return ResponseResult.Failed("Something went wrong");
            }

            var exists = await _projectRepository.ExistsAsync(x => x.ProjectName == entity.ProjectName);
            if (exists)
            {
                return ResponseResult.Exists("The project name already exists");
            }

            var result = await _projectRepository.AddAsync(entity);
            if (result == null)
            {
                return ResponseResult.Failed("Project was not created successfully");
            }

            return ResponseResult.Created("Project was created successfully");

        }
        catch (Exception ex)
        {
            return ResponseResult.Failed($"Error :: {ex.Message}");
        }
    }

    public async Task<ResponseResult<IEnumerable<Project>>> GetAllProjectsAsync()
    {
        try
        {
            var entities = await _projectRepository.GetAllAsync();
            if (entities == null)
            {
                return (ResponseResult<IEnumerable<Project>>)ResponseResult.Failed("Something went wrong wen fetching projects");
            }

            return ResponseResult<IEnumerable<Project>>.Ok(result: entities.Select(ProjectFactory.Create));
        }
        catch (Exception ex)
        {
            return (ResponseResult<IEnumerable<Project>>)ResponseResult.Failed($"Error :: {ex.Message}");
        }
    }

    public async Task<ResponseResult<Project>> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        try
        {
            if (expression == null)
            {
                return ResponseResult<Project>.NotFound("Invalid expression");
            }

            var entity = await _projectRepository.GetAsync(expression);
            if (entity == null)
            {
                return ResponseResult<Project>.NotFound("Couldn't found a project");
            }

            return ResponseResult<Project>.Ok(result: ProjectFactory.Create(entity));
        }
        catch (Exception ex)
        {
            return ResponseResult<Project>.NotFound($"Error :: {ex.Message}");
        }
    }

    public async Task<ResponseResult<ProjectEntity>> UpdateProjectAsync(Project project)
    {

        try
        {
            if (project == null)
            {
                return ResponseResult<ProjectEntity>.NotFound("Invalid model");
            }

            var entity = ProjectFactory.Create(project);
            var projectEntity = await _projectRepository.UpdateAsync(entity);

            if (projectEntity == null)
            {
                return ResponseResult<ProjectEntity>.NotFound("Couldn't update project");
            }

            return ResponseResult<ProjectEntity>.Ok(result: projectEntity);
        }
        catch (Exception ex)
        {
            return ResponseResult<ProjectEntity>.NotFound($"Error :: {ex.Message}");
        }
    }

    public async Task<ResponseResult> DeleteProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        try
        {
            if (expression == null)
            {
                return ResponseResult.Failed("Invalid expression");
            }

            var entity = await _projectRepository.GetAsync(expression);
            if (entity == null)
            {
                return ResponseResult.Failed("The project doesn't exists");
            }

            var result = await _projectRepository.RemoveAsync(entity);
            if (!result.HasValue || !result.Value)
            {
                return ResponseResult.Failed("Could't delete the project");
            }

            return ResponseResult.ActionSucceeded("Project has been deleted successfully");

        }
        catch (Exception ex)
        {
            return ResponseResult.Failed($"Error :: {ex.Message}");
        }
    }
}