﻿using Business.Factories;
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
            var entities = await _projectRepository.GetFilteredProjectsAsync();
            if (entities == null)
            {
                return (ResponseResult<IEnumerable<Project>>)ResponseResult.Failed("Something went wrong wen fetching projects");
            }

            var result = entities.Select(ProjectFactory.Create);
            return ResponseResult<IEnumerable<Project>>.Ok(result: result);
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

            var entity = await _projectRepository.GetProjectAsync(expression);
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

    public async Task<ResponseResult<Project>> UpdateProjectAsync(ProjectUpdateForm form)
    {

        try
        {
            var existingProject = await _projectRepository.ExistsAsync(x => x.Id == form.Id);
            if(!existingProject)
            {
                return ResponseResult<Project>.NotFound("Project not found");
            }

            var entity = ProjectFactory.Update(form);

            var updated = await _projectRepository.UpdateAsync(entity);
            if (!updated)
            {
                return ResponseResult<Project>.NotFound("Faild to update project");
            }

            var projectEntity = await _projectRepository.GetProjectAsync(x => x.Id == entity.Id);
            if (projectEntity == null)
            {
                return ResponseResult<Project>.NotFound("Faild to fetch project");
            }

            var project = ProjectFactory.Create(projectEntity);
            return ResponseResult<Project>.Ok(result: project);
        }
        catch (Exception ex)
        {
            return ResponseResult<Project>.NotFound($"Error :: {ex.Message}");
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