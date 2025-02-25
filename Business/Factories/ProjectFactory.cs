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
        };

        return entity;
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
            StatusTypeName = entity.StatusType.StatusType,
            ProjectManager = $"{entity.ProjectManager.FirstName} {entity.ProjectManager.LastName}"
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
        };

        return entity;
    }
}