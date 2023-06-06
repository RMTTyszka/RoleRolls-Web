using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Rolls.Application.Dtos;
using RoleRollsPocketEdition.Rolls.Domain.Services;

namespace RoleRollsPocketEdition.Rolls.Application.Controllers;

[Route("campaigns/{campaignId}/creatures/{creatureId}/chance")]
public class CdDiscoveryController : ControllerBase
{
    private readonly ICdDiscoveryService _cdDiscoveryService;
    private readonly RoleRollsDbContext _roleRollsDbContext;

    public CdDiscoveryController(ICdDiscoveryService cdDiscoveryService, RoleRollsDbContext roleRollsDbContext)
    {
        _cdDiscoveryService = cdDiscoveryService;
        _roleRollsDbContext = roleRollsDbContext;
    }

    [HttpPost]
    public async Task<List<CdDiscoveryResult>> GetCd([FromRoute] Guid creatureId, [FromBody] GetCdInput input)
    {
        var creature = await _roleRollsDbContext.Creatures
            .Include(creature => creature.Attributes)
            .Include(creature => creature.Lifes)
            .Include(creature => creature.Skills)
            .ThenInclude(skill => skill.MinorSkills)
            .FirstAsync(creature => creature.Id == creatureId);
        var property = creature.GetPropertyValue(input.PropertyType, input.PropertyId);
        var result = _cdDiscoveryService.GetDc(property.propertyValue, property.rollBonus, input.TargetChance);
        return result;
    }  
}