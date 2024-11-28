using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Domain.Campaigns.Repositories;
using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition._Domain.Itens.Configurations;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.Attacks.Services;

public class AttackService
{
    private readonly RoleRollsDbContext _context;
    private readonly ICreatureRepository _creatureRepository;

    public AttackService(RoleRollsDbContext context, ICreatureRepository creatureRepository)
    {
        _context = context;
        _creatureRepository = creatureRepository;
    }

    public async Task Attack(Guid campaignId, Guid sceneId, Guid attackerId, Guid targetId, AttackInput input)
    {
        var attacker = await _creatureRepository.GetFullCreature(attackerId);
        var target = await _creatureRepository.GetFullCreature(targetId);
        var command = new AttackCommand
        {
            ItemConfiguration = await _context.ItemConfigurations.FirstAsync(e => e.CampaignId == campaignId)
        };
        var attackResult = attacker.Attack(target, command);
    }
}

public class AttackInput
{
    public Guid DefenseId { get; set; }
    public EquipableSlot WeaponSlot { get; set; }
}
public class AttackCommand
{
    public EquipableSlot WeaponSlot { get; set; }
    public ItemConfiguration ItemConfiguration { get; set; }
}
public class AttackResult
{
    
}