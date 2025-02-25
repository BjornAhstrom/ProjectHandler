using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Contexts;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<DataContext>();
        optionBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\DatabasTeknik\\ProjectHandler\\Data\\Contexts\\Databases\\ProjectHandler_Database.mdf;Integrated Security=True;Connect Timeout=30");

        return new DataContext(optionBuilder.Options);
    }
}
