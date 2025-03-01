using Business.Models.Response;
using Business.Models.User;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<ResponseResult> AddAsync(UserRegistrationForm form);
        Task<ResponseResult> DeleteUserAsync(Expression<Func<UserEntity, bool>> expression);
        Task<ResponseResult<IEnumerable<User>>> GetAllAsync();
        Task<ResponseResult<User>> GetByIdAsync(int id);
        Task<ResponseResult<IEnumerable<User>>> GetProjectManagersAsync();
        Task<ResponseResult<User>> UpdateUserAsync(UserUpdateForm form);
    }
}