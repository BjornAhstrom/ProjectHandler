using Business.Factories;
using Business.Interfaces;
using Business.Models.Response;
using Business.Models.StatusType;
using Data.Interfaces;

namespace Business.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;

    public async Task<ResponseResult<IEnumerable<StatusType>>> GetAllAsync()
    {
        try
        {
            var entities = await _statusTypeRepository.GetAllAsync();
            if (entities == null)
            {
                return ResponseResult<IEnumerable<StatusType>>.NotFound("Couldn't found customers");
            }

            return ResponseResult<IEnumerable<StatusType>>.Ok(result: entities.Select(StatusTypeFactory.Create));


        } catch (Exception ex)
        {
            return ResponseResult<IEnumerable<StatusType>>.NotFound($"Error :: {ex.Message}");
        }
    }
}
