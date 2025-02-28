using Business.Factories;
using Business.Interfaces;
using Business.Models.Project;
using Business.Models.Response;
using Business.Models.User;
using Data.Entities;
using Data.Interfaces;
using System.Linq.Expressions;

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
            var entities = await _userRepository.GetAllAsync();
            if (entities == null)
            {
                return ResponseResult<IEnumerable<User>>.NotFound("Couldn't found any users");
            }

            return ResponseResult<IEnumerable<User>>.Ok(result: entities.Select(UserFactory.Create));

        }
        catch (Exception ex)
        {
            return ResponseResult<IEnumerable<User>>.NotFound($"Error :: {ex.Message}");
        }
    }

    public async Task<ResponseResult<User>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _userRepository.GetAsync(x => x.Id == id);
            if (entity == null)
            {
                return ResponseResult<User>.NotFound("");
            }

            var user = UserFactory.Create(entity);
            return ResponseResult<User>.Ok(result: user); ;
        }
        catch (Exception ex)
        {
            return ResponseResult<User>.NotFound($"Error :: {ex.Message}");
        }
    }

    public async Task<ResponseResult<IEnumerable<User>>> GetProjectManagersAsync()
    {
        try
        {
            var entities = await _userRepository.GetProjectManagersAsync();
            if (entities == null)
            {
                return ResponseResult<IEnumerable<User>>.NotFound("Couldn't found any project managers");
            }

            return ResponseResult<IEnumerable<User>>.Ok(result: entities.Select(UserFactory.Create));
        }
        catch (Exception ex)
        {
            return ResponseResult<IEnumerable<User>>.NotFound($"Error :: {ex.Message}");
        }
    }

    public async Task<ResponseResult<User>> UpdateUserAsync(UserUpdateForm form)
    {
        try
        {
            var existingUser = await _userRepository.ExistsAsync(x => x.Id == form.Id);
            if (!existingUser)
            {
                return ResponseResult<User>.NotFound("User not found");
            }

            var entity = UserFactory.Update(form);

            var updated = await _userRepository.UpdateAsync(entity);
            if (!updated)
            {
                return ResponseResult<User>.NotFound("Faild to update user");
            }

            var userEntity = await _userRepository.GetAsync(x => x.Id == entity.Id);
            if (userEntity == null)
            {
                return ResponseResult<User>.NotFound("Faild to fetch user");
            }

            var user = UserFactory.Create(userEntity);

            return ResponseResult<User>.Ok(result: user);
        }
        catch (Exception ex)
        {
            return ResponseResult<User>.NotFound($"Error :: {ex.Message}");
        }

        //return null!;
    }
}
