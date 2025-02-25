using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                StatusType = x.StatusType,
                ProjectManager = new UserEntity
                {
                    Id = x.ProjectManager.Id,
                    FirstName = x.ProjectManager.FirstName,
                    LastName = x.ProjectManager.LastName,
                }
                

            }).ToListAsync();

        return entities;
    }
}
