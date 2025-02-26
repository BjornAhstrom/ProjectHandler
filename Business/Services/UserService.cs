using Business.Factories;
using Business.Interfaces;
using Business.Models.Response;
using Business.Models.User;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;


    public async Task<ResponseResult> AddAsync(UserRegistrationForm form)
    {
        try
        {
            if (form == null)
            {
                return ResponseResult.Failed("Something went wrong");
            }
            var exists = await _userRepository.ExistsAsync(x => x.Email == form.Email);
            if (exists)
            {
                return ResponseResult.Exists("User alredy exists");
            }

            var result = await _userRepository.AddAsync(UserFactory.Create(form));
            if (result == null)
            {
                return ResponseResult.Failed("Couldn't create user");
            }

            return ResponseResult.Created("User created successfully");
        }
        catch (Exception ex)
        {
            return ResponseResult.Failed($"Error :: {ex.Message}");
        }
    }
    public async Task<ResponseResult<IEnumerable<User>>> GetAllAsync()
    {
        try
        {
            var entites = await _userRepository.GetAllAsync();
            if (entites == null)
            {
                return ResponseResult<IEnumerable<User>>.NotFound("Couldn't found any users");
            }

            return ResponseResult<IEnumerable<User>>.Ok(result: entites.Select(UserFactory.Create));

        }
        catch (Exception ex)
        {
            return ResponseResult<IEnumerable<User>>.NotFound($"Error :: {ex.Message}");
        }
    }
}
