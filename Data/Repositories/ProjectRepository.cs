using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    public async Task<IEnumerable<ProjectEntity>> GetFilteredProjectsAsync()
    {
        var entities = await _context.Projects
            .Include(x => x.StatusType)
            .Include(x => x.ProjectManager)
            .Select(x => new  ProjectEntity
            {
                Id = x.Id,
                ProjectName = x.ProjectName,
                Description = x.Description,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                StatusTypeId = x.StatusTypeId,
                CustomerId = x.CustomerId,
                ProjectManagerId = x.ProjectManagerId,
                StatusType = x.StatusType,
                ProjectManager = new UserEntity
                {
                    Id = x.ProjectManager!.Id,
                    FirstName = x.ProjectManager.FirstName,
                    LastName = x.ProjectManager.LastName,
                }
                

            }).ToListAsync();

        return entities;
    }

    public async Task<ProjectEntity?> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entities = await _context.Projects
           .Include(x => x.StatusType)
           .Include(x => x.ProjectManager)
           .Select(x => new ProjectEntity
           {
               Id = x.Id,
               ProjectName = x.ProjectName,
               Description = x.Description,
               StartDate = x.StartDate,
               EndDate = x.EndDate,
               StatusTypeId = x.StatusTypeId,
               CustomerId = x.CustomerId,
               ProjectManagerId = x.ProjectManagerId,
               StatusType = x.StatusType,
               ProjectManager = new UserEntity
               {
                   Id = x.ProjectManager!.Id,
                   FirstName = x.ProjectManager.FirstName,
                   LastName = x.ProjectManager.LastName,
               }


           }).FirstOrDefaultAsync(expression);

        return entities;
    }

    public async Task<ProjectEntity?> UpdateByIdAsync(Expression<Func<ProjectEntity, bool>> expression, int statusTypeId)
    {
       try
        {
            var entity = await _context.Projects
           .Include(x => x.StatusType)
           .FirstOrDefaultAsync(expression);

            if (entity == null)
            {
                return null;
            }

            entity.StatusTypeId = statusTypeId;

            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null;
        }
    }
}
