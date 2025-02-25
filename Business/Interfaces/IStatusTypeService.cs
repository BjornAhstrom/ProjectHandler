using Business.Models.Response;
using Business.Models.StatusType;

namespace Business.Interfaces;

public interface IStatusTypeService
{
    Task<ResponseResult<IEnumerable<StatusType>>> GetAllAsync();
}
