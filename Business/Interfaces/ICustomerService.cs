using Business.Models.Customer;
using Business.Models.Response;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<ResponseResult<IEnumerable<Customer>>> GetAllAsync();
    }
}