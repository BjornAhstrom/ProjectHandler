using Business.Models.Project;
using Business.Models.Response;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<ResponseResult> AddAsync(ProjectRegistrationForm form);
        Task<ResponseResult> DeleteProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
        Task<ResponseResult<IEnumerable<Project>>> GetAllProjectsAsync();
        Task<ResponseResult<Project>> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
        Task<ResponseResult<Project>> UpdateProjectAsync(ProjectEntity entity);
    }
}