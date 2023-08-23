using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RoleRollsPocketEdition.Infrastructure;

public class RoleRollsDbContextDesignTime 
  //   : IDesignTimeDbContextFactory<RoleRollsDbContext>
{
    private readonly IConfiguration _configuration;

    public RoleRollsDbContext CreateDbContext(string[] args)
    {

        var optionsBuilder = new DbContextOptionsBuilder<RoleRollsDbContext>();
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("RoleRolls") ?? string.Empty);
        return new RoleRollsDbContext(optionsBuilder.Options, _configuration);
    }
}