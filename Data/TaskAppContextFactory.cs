using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class TaskAppContextFactory : IDesignTimeDbContextFactory<TaskAppContext>
{
    public TaskAppContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TaskAppContext>();

        // Burada migration işlemi sırasında kullanılacak bağlantı dizgesini sağlıyoruz.
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultSQLConnection");
        optionsBuilder.UseSqlServer(connectionString);

        return new TaskAppContext(optionsBuilder.Options);
    }
}
