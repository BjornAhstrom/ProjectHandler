using Business.Models.Response;
using Business.Models.User;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<ResponseResult> AddAsync(UserRegistrationForm form);
        Task<ResponseResult<IEnumerable<User>>> GetAllAsync();
        Task<ResponseResult<User>> GetByIdAsync(int id);
    }
}