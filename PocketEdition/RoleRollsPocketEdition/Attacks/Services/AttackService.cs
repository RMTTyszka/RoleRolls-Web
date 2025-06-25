using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Repositories;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Powers.Entities;
using RoleRollsPocketEdition.Powers.Models;
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
        var attacker = await LoadCreature(attackerId);
        var target = await LoadCreature(input.TargetId);
        var itemConfiguration = await LoadItemConfiguration(campaignId);
        var command = BuildAttackCommand(itemConfiguration, input);
        var attackResult = attacker.Attack(target, command);
        await _scenesService.ProcessAction(sceneId, attackResult);
    }

    private async Task<Creature> LoadCreature(Guid creatureId)
    {
        var creature = await _creatureRepository.GetFullCreature(creatureId);
        if (creature == null)
            throw new InvalidOperationException($"Creature not found: {creatureId}");
        return creature;
    }

    private async Task<ItemConfiguration> LoadItemConfiguration(Guid campaignId)
    {
        return await _context.Campaigns
            .AsNoTracking()
            .Include(e => e.CampaignTemplate)
            .ThenInclude(e => e.ItemConfiguration)
            .Where(e => e.Id == campaignId)
            .Select(e => e.CampaignTemplate.ItemConfiguration)
            .FirstAsync();
    }

    private AttackCommand BuildAttackCommand(ItemConfiguration config, AttackInput input)
    {
        return new AttackCommand
        {
            ItemConfiguration = config,
            WeaponSlot = input.WeaponSlot,
            DefenseId = input.Defense,
            VitalityId = input.Vitality,
            CombatManeuverIds = input.CombatManeuverIds,
            Luck = input.Luck,
            Advantage = input.Advantage,
            HitProperty = input.HitProperty,
            HitAttribute = input.HitAttribute,
            DamageAttribute = input.DamageAttribute,
        };
    }

}

public class AttackInput
{
    public Guid TargetId { get; set; }
    public Property? Defense { get; set; }
    public Property? Vitality { get; set; }
    public Property? HitProperty { get; set; }
    public Property? DamageAttribute { get; set; }
    public EquipableSlot WeaponSlot { get; set; }
    public List<Guid> CombatManeuverIds { get; set; } = [];
    public int Luck { get; set; }
    public int Advantage { get; set; }
    public Property? HitAttribute { get; set; }
}
public class AttackCommand
{
    public EquipableSlot WeaponSlot { get; set; }
    public ItemConfiguration ItemConfiguration { get; set; }
    public Property? DefenseId { get; set; }
    public Property? HitProperty { get; set; }
    public Property? HitAttribute { get; set; }
    public Property? VitalityId { get; set; }
    public Property? SecondVitalityId { get; set; }
    public Property? DamageAttribute { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
    public Guid GetDefenseId => DefenseId?.Id ?? ItemConfiguration.ArmorProperty.Id;
    public Guid GetVitalityId => VitalityId?.Id ?? ItemConfiguration.BasicAttackTargetFirstVitality.Id;
    public Guid GetSecondVitalityId => SecondVitalityId?.Id ?? ItemConfiguration.BasicAttackTargetSecondVitality.Id;
    public List<Guid> CombatManeuverIds { get; set; } = [];
}
public class AttackResult
{
    public Creature Attacker { get; set; }
    public Creature Target { get; set; }
    public object TotalDamage { get; set; }
    public ItemInstance Weapon { get; set; }
    public bool Success { get; set; }
}