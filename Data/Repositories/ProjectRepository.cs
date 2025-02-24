using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context)
{
    public async Task<IEnumerable<ProjectEntity>> GetFilteredProjectsAsync()
    {
        var entities = await _context.Projects
            .Include(x => x.Status)
            .Include(x => x.ProjectManager)
            .Select(x => new  ProjectEntity
            {
                Id = x.Id,
                ProjectName = x.ProjectName,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = new StatusTypeEntity
                {
                    StatusType = x.Status.StatusType
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
