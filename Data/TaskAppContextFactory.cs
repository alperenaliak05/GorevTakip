using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class TaskAppContextFactory : IDesignTimeDbContextFactory<TaskAppContext>
{
    public TaskAppContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TaskAppContext>();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../TaskApp"))
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultSQLConnection");
        optionsBuilder.UseSqlServer(connectionString);

        return new TaskAppContext(optionsBuilder.Options);
    }
}
