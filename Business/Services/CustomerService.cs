using Business.Factories;
using Business.Interfaces;
using Business.Models.Customer;
using Business.Models.Response;
using Data.Interfaces;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<ResponseResult<IEnumerable<Customer>>> GetAllAsync()
    {
        try
        {
            var entities = await _customerRepository.GetAllAsync();
            if (entities == null)
            {
                return ResponseResult<IEnumerable<Customer>>.NotFound("Couldn't found any customers");
            }

            var customers = ResponseResult<IEnumerable<Customer>>.Ok(result: entities.Select(CustomerFactory.Create));
            return customers;

        }
        catch (Exception ex)
        {
            return ResponseResult<IEnumerable<Customer>>.NotFound($"Error :: {ex.Message}");
        }
    }
}
