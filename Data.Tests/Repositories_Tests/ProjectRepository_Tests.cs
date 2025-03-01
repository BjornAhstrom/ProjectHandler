using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.Repositories_Tests;

public class ProjectRepository_Tests
{
    private DataContext GetDataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new DataContext(options);
        context.Database.EnsureCreated();
        return context;
    }


    [Fact]
    public async Task AddAsync_ShouldAddAndReturnProject()
    {
        // Arrange
        var context = GetDataContext();
        context.StatusTypes.AddRange(TestData.StatusesEntities);
        context.Customers.AddRange(TestData.CustomersEntities);
        context.Roles.AddRange(TestData.RolesEntities);
        context.Users.AddRange(TestData.UsersEntities);
        context.UserRoles.AddRange(TestData.UserRoleEntities);

        await context.SaveChangesAsync();
        IProjectRepository repository = new ProjectRepository(context);

        var projectEntity = TestData.ProjectEntities[0];
        // Act
        var result = await repository.AddAsync(projectEntity);

        // Assert
        Assert.NotEqual(0, result!.Id);
    }

}
