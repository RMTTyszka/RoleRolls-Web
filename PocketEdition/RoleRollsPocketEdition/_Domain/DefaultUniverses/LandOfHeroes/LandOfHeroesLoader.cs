using RoleRollsPocketEdition._Domain.Global;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Domain.DefaultUniverses.LandOfHeroes;

public class LandOfHeroesLoader : IStartupTask
{
    private readonly RoleRollsDbContext _dbContext;

    public LandOfHeroesLoader(RoleRollsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        
    }
}