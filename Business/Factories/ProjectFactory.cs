using Business.Models.Project;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectEntity Create(ProjectRegistrationForm form)
    {
        var entity = new ProjectEntity
        {
            ProjectName = form.ProjectName,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            CustomerId = form.CustomerId,
            StatusTypeId = form.StatusTypeId,
            ProjectManagerId = form.ProjectManagerId
        };

        return entity;
    }

    public static ProjectEntity Update(ProjectUpdateForm form)
    {
        var project = new ProjectEntity
        {
            Id = form.Id,
            ProjectName = form.ProjectName,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            StatusTypeId = form.StatusTypeId,
            CustomerId = form.CustomerId,
            ProjectManagerId = form.ProjectManagerId

        };

        return project;
    }
    public static Project Create(ProjectEntity entity)
    {
        var project = new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            StatusTypeName = entity.StatusType!.StatusType,
            ProjectManager = $"{entity.ProjectManager!.FirstName} {entity.ProjectManager.LastName}",
            StatusTypeId = entity.StatusTypeId,
            CustomerId = entity.CustomerId,
            ProjectManagerId = entity.ProjectManagerId

        };

        return project;
    }

    public static ProjectEntity Create(Project project)
    {
        var entity = new ProjectEntity
        {
            Id = project.Id,
            ProjectName = project.ProjectName,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            StatusTypeId = project.StatusTypeId,
            CustomerId = project.CustomerId,
            ProjectManagerId = project.ProjectManagerId
        };

        return entity;
    }
}