using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.Repositories_Tests;

public class StatusTypeRepository_Tests
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
    public async Task GetStatusTypesAsync_ShouldReturnAllStatusTypes()
    {
        // Arrange
        var context = GetDataContext();
        context.StatusTypes.AddRange(TestData.StatusesEntities);
        await context.SaveChangesAsync();

        IStatusTypeRepository repository = new StatusTypeRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.StatusesEntities.Length, result.Count());

        context.Dispose();
        context = null!;
    }
}
