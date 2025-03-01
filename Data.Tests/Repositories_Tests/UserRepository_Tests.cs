using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.Repositories_Tests;

public class UserRepository_Tests
{
    private DataContext GetDataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new DataContext(options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task AddAsync_ShouldAddAndReturnUser()
    {
        // Arrange
        var context = GetDataContext();
        context.Roles.AddRange(TestData.RolesEntities);
        context.Users.AddRange(TestData.UsersEntities);
        context.UserRoles.AddRange(TestData.UserRoleEntities);
        await context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(context);

        // Tog hjälp av ChatGpt
        var userEntity = new UserEntity
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test.user@example.com",
            RoleId = 1,
            Role = context.Roles.First(r => r.Id == 1)
        };
        var existingProject = await context.Users.FirstOrDefaultAsync(x => x.Id == userEntity.Id);
        if (existingProject != null)
        {
            await repository.RemoveAsync(existingProject);
            await context.SaveChangesAsync();
        }

        // Act
        var result = await repository.AddAsync(userEntity);


        // Assert
        Assert.NotEqual(0, result!.Id);
        Assert.Contains(userEntity, context.Users);

        context.Dispose();
        context = null!;
    }

    [Fact]
    public async Task GetUsersAsync_ShouldReturnAllUsers()
    {
        // Arrange
        var context = GetDataContext();
        context.Roles.AddRange(TestData.RolesEntities);
        context.Users.AddRange(TestData.UsersEntities);
        context.UserRoles.AddRange(TestData.UserRoleEntities);
        await context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.UsersEntities.Length, result.Count());

        context.Dispose();
        context = null!;
    }

    [Fact]
    public async Task GetUserById_ShouldReturnOneUser()
    {
        // Arrange
        var context = GetDataContext();
        context.Roles.AddRange(TestData.RolesEntities);
        context.Users.AddRange(TestData.UsersEntities);
        context.UserRoles.AddRange(TestData.UserRoleEntities);
        await context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(context);

        // Act

        var result = await repository.GetAsync(x => x.Id == 1);

        // Assert
        Assert.Equal(TestData.UsersEntities[0].Id, result!.Id);

        context.Dispose();
        context = null!;
    }

    [Fact]
    public async Task GetProjectManager_ShouldReturnAllProjectManagers()
    {
        // Arrange
        var context = GetDataContext();
        context.Roles.AddRange(TestData.RolesEntities);
        context.Users.AddRange(TestData.UsersEntities);
        context.UserRoles.AddRange(TestData.UserRoleEntities);
        await context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(context);

        // Act

        var result = await repository.GetProjectManagersAsync();
        var expected = TestData.UsersEntities.Count(x => x.UserRoles.Any(ur => ur.Role.RoleName == "Projektledare"));

        // Assert
        Assert.Equal(expected, result.Count());

        context.Dispose();
        context = null!;
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnTrue()
    {
        // Arrange
        var context = GetDataContext();
        context.Roles.AddRange(TestData.RolesEntities);
        context.Users.AddRange(TestData.UsersEntities);
        context.UserRoles.AddRange(TestData.UserRoleEntities);
        await context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(context);


        // Act
        var oldUserInfo = TestData.UsersEntities[0];
        var newUserInfo = TestData.UpdatedUsersEntities[0];

        var result = await repository.UpdateAsync(oldUserInfo);

        // Assert
        Assert.NotEqual(oldUserInfo.FirstName, newUserInfo.FirstName);
        Assert.NotEqual(oldUserInfo.Email, newUserInfo.Email);
        Assert.True(result);

        context.Dispose();
        context = null!;
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnTrue()
    {
        // Arrange
        var context = GetDataContext();
        context.Users.AddRange(TestData.UsersEntities);
        context.Roles.AddRange(TestData.RolesEntities);
        context.UserRoles.AddRange(TestData.UserRoleEntities);
        await context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(context);

        // Act
        var user = TestData.UsersEntities[0];
        var result = await repository.RemoveAsync(user);

        // Assert
        Assert.True(result);

        var deletedUser = await context.Users.FindAsync(user.Id);
        Assert.Null(deletedUser);

        context.Dispose();
        context = null!;
    }

}