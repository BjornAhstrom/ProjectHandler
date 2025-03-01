using Data.Contexts;
using Data.Entities;
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
        var existingProject = await context.Projects.FirstOrDefaultAsync(x => x.Id == projectEntity.Id);
        if (existingProject != null)
        {
            await repository.RemoveAsync(existingProject);
            await context.SaveChangesAsync();
        }
        // Act
        var result = await repository.AddAsync(projectEntity);

        // Assert
        Assert.NotEqual(0, result!.Id);
        Assert.Contains(projectEntity, context.Projects);

        context.Dispose();
        context = null!;
    }


    [Fact]
    public async Task GetProjectsAsync_ShouldReturnAllPojects()
    {
        // Arrange
        var context = GetDataContext();
        context.StatusTypes.AddRange(TestData.StatusesEntities);
        context.Customers.AddRange(TestData.CustomersEntities);
        context.Roles.AddRange(TestData.RolesEntities);
        context.Users.AddRange(TestData.UsersEntities);
        context.UserRoles.AddRange(TestData.UserRoleEntities);
        context.Projects.AddRange(TestData.ProjectEntities);
        await context.SaveChangesAsync();

        IProjectRepository repository = new ProjectRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.ProjectEntities.Length, result.Count());

        context.Dispose();
        context = null!;
    }

    [Fact]
    public async Task GetProjectById_ShouldReturnOneProject()
    {
        // Arrange
        var context = GetDataContext();
        context.StatusTypes.AddRange(TestData.StatusesEntities);
        context.Customers.AddRange(TestData.CustomersEntities);
        context.Roles.AddRange(TestData.RolesEntities);
        context.Users.AddRange(TestData.UsersEntities);
        context.UserRoles.AddRange(TestData.UserRoleEntities);
        context.Projects.AddRange(TestData.ProjectEntities);
        await context.SaveChangesAsync();

        IProjectRepository repository = new ProjectRepository(context);

        // Act

        var result = await repository.GetAsync(x => x.Id == 1);

        // Assert
        Assert.Equal(TestData.ProjectEntities[0].Id, result!.Id);

        context.Dispose();
        context = null!;
    }


    [Fact]
    public async Task UpdateProject_ShouldReturnTrue()
    {
        // Arrange
        var context = GetDataContext();
        context.StatusTypes.AddRange(TestData.StatusesEntities);
        context.Customers.AddRange(TestData.CustomersEntities);
        context.Roles.AddRange(TestData.RolesEntities);
        context.Users.AddRange(TestData.UsersEntities);
        context.UserRoles.AddRange(TestData.UserRoleEntities);
        context.Projects.AddRange(TestData.ProjectEntities);
        await context.SaveChangesAsync();

        IProjectRepository repository = new ProjectRepository(context);


        // Act
        var oldProjectInfo = TestData.ProjectEntities[0];
        var newProjectInfo = TestData.NewProjectEntities[0];

        var result = await repository.UpdateAsync(oldProjectInfo);

        // Assert
        Assert.NotEqual(oldProjectInfo.ProjectName, newProjectInfo.ProjectName);
        Assert.NotEqual(oldProjectInfo.Description, newProjectInfo.Description);
        Assert.True(result);

        context.Dispose();
        context = null!;
    }

    [Fact]
    public async Task DeleteProject_ShouldReturnTrue()
    {
        // Arrange
        var context = GetDataContext();
        context.Projects.AddRange(TestData.ProjectEntities);
        await context.SaveChangesAsync();

        IProjectRepository repository = new ProjectRepository(context);

        // Act
        var project = TestData.ProjectEntities[0];
        var result = await repository.RemoveAsync(project);

        // Assert
        Assert.True(result);

        var deletedProject= await context.Projects.FindAsync(project.Id);
        Assert.Null(deletedProject);

        context.Dispose();
        context = null!;
    }

}
