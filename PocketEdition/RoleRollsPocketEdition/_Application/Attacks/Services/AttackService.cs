using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Application.Scenes.Services;
using RoleRollsPocketEdition._Domain.Campaigns.Repositories;
using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition._Domain.Itens.Configurations;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.Attacks.Services;

public class AttackService
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
            ItemConfiguration = await _context.ItemConfigurations.FirstAsync(e => e.CampaignId == campaignId),
            WeaponSlot = input.WeaponSlot,
            DefenseId = input.DefenseId,
            LifeId = input.LifeId,
        };
        var attackResult = attacker.Attack(target, command);
        _scenesService.ProcessAction(sceneId, attackResult);
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
    public Guid GetDefenseId => DefenseId ?? ItemConfiguration.ArmorDefenseId.Value;
    public Guid GetLifeId => LifeId ?? ItemConfiguration.BasicAttackTargetLifeId.Value;
}
public class AttackResult
{
    public Creature Attacker { get; set; }
    public Creature Target { get; set; }
    public object TotalDamage { get; set; }
    public ItemInstance Weapon { get; set; }
    public bool Success { get; set; }
}