using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IProjectRepository : IBaseRepository<ProjectEntity>
{
    Task<IEnumerable<ProjectEntity>> GetFilteredProjectsAsync();
    Task<ProjectEntity?> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<ProjectEntity?> UpdateByIdAsync(Expression<Func<ProjectEntity, bool>> expression, int statusTypeId);
}
