using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RoleRollsPocketEdition.Infrastructure;

public class RoleRollsDbContextDesignTime : IDesignTimeDbContextFactory<RoleRollsDbContext>
{
    public RoleRollsDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<RoleRollsDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("RoleRolls") ?? string.Empty);
        return new RoleRollsDbContext(optionsBuilder.Options, configuration);
    }
}
