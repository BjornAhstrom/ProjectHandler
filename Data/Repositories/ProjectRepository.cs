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
                StatusType = new StatusTypeEntity
                {
                    StatusType = x.StatusType.StatusType
                },
                ProjectManager = new UserEntity
                {
                    FirstName = x.ProjectManager.FirstName,
                    LastName = x.ProjectManager.LastName,
                }
                

            }).ToListAsync();

        return entities;
    }
}
