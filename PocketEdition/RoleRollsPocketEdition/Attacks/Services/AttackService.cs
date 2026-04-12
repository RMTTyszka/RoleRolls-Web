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
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.Scenes.Services;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Attacks.Services;

public interface IAttackService
{
    Task Attack(Guid campaignId, Guid sceneId, Guid attackerId, AttackInput input);
}

public class AttackService : IAttackService, ITransientDependency
{
    private readonly RoleRollsDbContext _context;
    private readonly ICreatureRepository _creatureRepository;
    private readonly IScenesService _scenesService;
    private readonly IDiceRoller _diceRoller;

    public AttackService(RoleRollsDbContext context, ICreatureRepository creatureRepository,
        IScenesService scenesService, IDiceRoller diceRoller)
    {
        _context = context;
        _creatureRepository = creatureRepository;
        _scenesService = scenesService;
        _diceRoller = diceRoller;
    }

    public async Task Attack(Guid campaignId, Guid sceneId, Guid attackerId, AttackInput input)
    {
        var attacker = await LoadCreature(attackerId);
        var target = await LoadCreature(input.TargetId);
        var attackConfiguration = await LoadAttackConfiguration(campaignId);
        var command = BuildAttackCommand(attackConfiguration, input);
        var attackResult = attacker.Attack(target, command, _diceRoller);
        await _scenesService.ProcessAction(sceneId, attackResult);
    }

    private async Task<Creature> LoadCreature(Guid creatureId)
    {
        var creature = await _creatureRepository.GetFullCreature(creatureId);
        if (creature == null)
            throw new InvalidOperationException($"Creature not found: {creatureId}");
        return creature;
    }

    private async Task<AttackConfiguration> LoadAttackConfiguration(Guid campaignId)
    {
        var campaignTemplate = await _context.Campaigns
            .AsNoTracking()
            .Include(e => e.CampaignTemplate)
            .ThenInclude(e => e.ItemConfiguration)
            .Where(e => e.Id == campaignId)
            .Select(e => e.CampaignTemplate)
            .FirstAsync();

        return new AttackConfiguration
        {
            ItemConfiguration = campaignTemplate.ItemConfiguration
        };
    }

    private AttackCommand BuildAttackCommand(AttackConfiguration config, AttackInput input)
    {
        return new AttackCommand
        {
            ItemConfiguration = config.ItemConfiguration,
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

public class AttackConfiguration
{
    public ItemConfiguration ItemConfiguration { get; set; } = null!;
}

public class AttackInput
{
    public Guid TargetId { get; set; }
    public Guid? Defense { get; set; }
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
    public ItemConfiguration ItemConfiguration { get; set; } = null!;
    public List<BasicAttackVitalityRule> BasicAttackVitalityRules { get; set; } = [];
    public Guid? DefenseId { get; set; }
    public Property? HitProperty { get; set; }
    public Property? HitAttribute { get; set; }
    public Property? VitalityId { get; set; }
    public Property? SecondVitalityId { get; set; }
    public Property? DamageAttribute { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }

    public Guid GetDefenseId1 =>
        DefenseId ?? ItemConfiguration.ArmorDefense1 ??
        throw new InvalidOperationException("Armor property is not configured for this campaign.");

    public Guid GetDefenseId2 =>
        DefenseId ?? ItemConfiguration.ArmorDefense2 ??
        throw new InvalidOperationException("Armor property is not configured for this campaign.");

    public Guid GetVitalityId =>
        GetBasicAttackVitalityRules.FirstOrDefault()?.Vitality?.Id ??
        throw new InvalidOperationException("Primary target vitality is not configured for this campaign.");

    public Guid GetSecondVitalityId =>
        GetBasicAttackVitalityRules.Skip(1).FirstOrDefault()?.Vitality?.Id ??
        throw new InvalidOperationException("Secondary target vitality is not configured for this campaign.");

    public IReadOnlyList<BasicAttackVitalityRule> GetBasicAttackVitalityRules
    {
        get
        {
            var configuredRules = BasicAttackVitalityRules
                .Where(rule => rule.Vitality != null)
                .Select(rule => rule.Clone())
                .ToList();

            if (VitalityId == null)
            {
                return configuredRules;
            }

            var overrideRule = configuredRules.FirstOrDefault(rule => rule.Vitality?.Id == VitalityId.Id) ??
                               new BasicAttackVitalityRule
                               {
                                   Vitality = new Property(VitalityId.Id, VitalityId.Type ?? PropertyType.Vitality)
                               };

            var orderedRules = new List<BasicAttackVitalityRule> { overrideRule };
            orderedRules.AddRange(configuredRules
                .Where(rule => rule.Vitality?.Id != overrideRule.Vitality?.Id)
                .Select(rule => rule.Clone()));

            return orderedRules;
        }
    }

    public List<Guid> CombatManeuverIds { get; set; } = [];
    public Property? BlockProperty { get; set; }
}

public class AttackResult
{
    public Creature Attacker { get; set; } = null!;
    public Creature Target { get; set; } = null!;
    public int TotalDamage { get; set; }
    public ItemInstance Weapon { get; set; } = null!;
    public bool Success { get; set; }
    public int Block { get; set; }
    public int DamageBonus { get; set; }
    public int NumberOfRollSuccesses { get; set; }
    public int Difficulty { get; set; }
}
