using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Repositories;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Scenes.Services;

namespace RoleRollsPocketEdition.Attacks.Services;

public interface IAttackService
{
    Task Attack(Guid campaignId, Guid sceneId, Guid attackerId,  AttackInput input);
}

public class AttackService : IAttackService, ITransientDependency
{
    private readonly RoleRollsDbContext _context;
    private readonly ICreatureRepository _creatureRepository;
    private readonly IScenesService _scenesService;

    public AttackService(RoleRollsDbContext context, ICreatureRepository creatureRepository, IScenesService scenesService)
    {
        _context = context;
        _creatureRepository = creatureRepository;
        _scenesService = scenesService;
    }

    public async Task Attack(Guid campaignId, Guid sceneId, Guid attackerId, AttackInput input)
    {
        var attacker = await _creatureRepository.GetFullCreature(attackerId);   
        var target = await _creatureRepository.GetFullCreature(input.TargetId);
        var command = new AttackCommand
        {
            ItemConfiguration = await _context.Campaigns
                .Include(e => e.CampaignTemplate)
                .ThenInclude(e => e.ItemConfiguration).Where(e => e.Id == campaignId)
                .Select(e => e.CampaignTemplate.ItemConfiguration)
                .FirstAsync(),
            WeaponSlot = input.WeaponSlot,
            DefenseId = input.DefenseId,
            LifeId = input.LifeId,
        };
        var attackResult = attacker.Attack(target, command);
        await _scenesService.ProcessAction(sceneId, attackResult);
    }
}

public class AttackInput
{
    public Guid TargetId { get; set; }
    public Guid? DefenseId { get; set; }
    public Guid? LifeId { get; set; }
    public EquipableSlot WeaponSlot { get; set; }
}
public class AttackCommand
{
    public EquipableSlot WeaponSlot { get; set; }
    public ItemConfiguration ItemConfiguration { get; set; }
    public Guid? DefenseId { get; set; }
    public Guid? LifeId { get; set; }
    public Guid GetDefenseId => DefenseId ?? ItemConfiguration.ArmorProperty.PropertyId;
    public Guid GetLifeId => LifeId ?? ItemConfiguration.BasicAttackTargetFirstLife.PropertyId;
}
public class AttackResult
{
    public Creature Attacker { get; set; }
    public Creature Target { get; set; }
    public object TotalDamage { get; set; }
    public ItemInstance Weapon { get; set; }
    public bool Success { get; set; }
}