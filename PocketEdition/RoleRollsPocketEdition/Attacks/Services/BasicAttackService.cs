using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Attacks.Models;
using RoleRollsPocketEdition.Campaigns.Repositories;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.Scenes.Services;

namespace RoleRollsPocketEdition.Attacks.Services;

public interface IBasicAttackService
{
    Task<BasicAttackResponse> Attack(Guid campaignId, Guid sceneId, Guid attackerId, BasicAttackInput input);
}

public class BasicAttackService : IBasicAttackService, ITransientDependency
{
    private readonly RoleRollsDbContext _context;
    private readonly ICreatureRepository _creatureRepository;
    private readonly IScenesService _scenesService;
    private readonly IDiceRoller _diceRoller;

    public BasicAttackService(
        RoleRollsDbContext context,
        ICreatureRepository creatureRepository,
        IScenesService scenesService,
        IDiceRoller diceRoller)
    {
        _context = context;
        _creatureRepository = creatureRepository;
        _scenesService = scenesService;
        _diceRoller = diceRoller;
    }

    public async Task<BasicAttackResponse> Attack(Guid campaignId, Guid sceneId, Guid attackerId, BasicAttackInput input)
    {
        var attacker = await LoadCreature(attackerId);
        var target = await LoadCreature(input.TargetId);
        var itemConfiguration = await LoadItemConfiguration(campaignId);
        var command = BuildCommand(itemConfiguration, input);
        var attackResult = attacker.BasicAttack(target, command, _diceRoller);

        await _scenesService.ProcessBasicAttackAction(sceneId, attackResult);
        return BasicAttackResponse.From(attackResult);
    }

    private async Task<Creature> LoadCreature(Guid creatureId)
    {
        var creature = await _creatureRepository.GetFullCreature(creatureId);
        if (creature == null)
        {
            throw new InvalidOperationException($"Creature not found: {creatureId}");
        }

        return creature;
    }

    private async Task<ItemConfiguration> LoadItemConfiguration(Guid campaignId)
    {
        return await _context.Campaigns
            .AsNoTracking()
            .Include(campaign => campaign.CampaignTemplate)
            .ThenInclude(template => template.ItemConfiguration)
            .Where(campaign => campaign.Id == campaignId)
            .Select(campaign => campaign.CampaignTemplate.ItemConfiguration)
            .FirstAsync();
    }

    private static BasicAttackCommand BuildCommand(ItemConfiguration itemConfiguration, BasicAttackInput input)
    {
        return new BasicAttackCommand
        {
            WeaponSlot = input.WeaponSlot,
            ItemConfiguration = itemConfiguration,
            DefenseId = input.DefenseId,
            VitalityId = input.VitalityId,
            CombatManeuverIds = input.CombatManeuverIds,
            Luck = input.Luck,
            Advantage = input.Advantage
        };
    }
}

public class BasicAttackCommand
{
    public EquipableSlot WeaponSlot { get; set; }
    public ItemConfiguration ItemConfiguration { get; set; } = null!;
    public Guid? DefenseId { get; set; }
    public Property? VitalityId { get; set; }
    public List<Guid> CombatManeuverIds { get; set; } = [];
    public int Luck { get; set; }
    public int Advantage { get; set; }

    public Guid ResolvedDefenseId =>
        DefenseId ?? ItemConfiguration.ArmorDefense1 ??
        throw new InvalidOperationException("Armor property is not configured for this campaign.");
}

public class BasicAttackResult
{
    public Creature Attacker { get; set; } = null!;
    public Creature Target { get; set; } = null!;
    public EquipableSlot WeaponSlot { get; set; }
    public ItemInstance Weapon { get; set; } = null!;
    public bool Success { get; set; }
    public Guid DefenseId { get; set; }
    public int Complexity { get; set; }
    public int Difficulty { get; set; }
    public int NumberOfSuccesses { get; set; }
    public int NumberOfRollSuccesses { get; set; }
    public int Bonus { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
    public string RolledDices { get; set; } = string.Empty;
    public int TotalDamage { get; set; }
    public int Block { get; set; }
    public int DamageBonus { get; set; }
}
