using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Attacks.Models;
using RoleRollsPocketEdition.Campaigns.Repositories;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.Scenes.Services;

namespace RoleRollsPocketEdition.Attacks.Services;

public interface IEvadeService
{
    Task<EvadeResponse> Evade(Guid campaignId, Guid sceneId, Guid defenderId, EvadeInput input);
}

public class EvadeService : IEvadeService, ITransientDependency
{
    private readonly RoleRollsDbContext _context;
    private readonly ICreatureRepository _creatureRepository;
    private readonly IScenesService _scenesService;
    private readonly IDiceRoller _diceRoller;

    public EvadeService(
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

    public async Task<EvadeResponse> Evade(Guid campaignId, Guid sceneId, Guid defenderId, EvadeInput input)
    {
        var defender = await LoadCreature(defenderId);
        var attacker = await LoadCreature(input.AttackerId);
        var itemConfiguration = await LoadItemConfiguration(campaignId);
        var command = new EvadeCommand
        {
            WeaponSlot = input.WeaponSlot,
            ItemConfiguration = itemConfiguration,
            VitalityId = input.VitalityId,
            Luck = input.Luck,
            Advantage = input.Advantage
        };
        var evadeResult = defender.Evade(attacker, command, _diceRoller);

        await _scenesService.ProcessEvadeAction(sceneId, evadeResult);
        return EvadeResponse.From(evadeResult);
    }

    private async Task<Creature> LoadCreature(Guid creatureId)
    {
        var creature = await _creatureRepository.GetFullCreature(creatureId);
        if (creature is null)
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
}

public class EvadeCommand
{
    public EquipableSlot WeaponSlot { get; set; }
    public ItemConfiguration ItemConfiguration { get; set; } = null!;
    public Property? VitalityId { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
}

public class EvadeResult
{
    public Creature Attacker { get; set; } = null!;
    public Creature Defender { get; set; } = null!;
    public ItemInstance Weapon { get; set; } = null!;
    public EquipableSlot WeaponSlot { get; set; }
    public bool Success { get; set; }
    public int BaseDice { get; set; }
    public int Difficulty { get; set; }
    public int EvadeBonus { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
    public string RolledDices { get; set; } = string.Empty;
    public List<int> KeptResults { get; set; } = [];
    public List<int> Excesses { get; set; } = [];
    public int NumberOfHits { get; set; }
    public int Block { get; set; }
    public int DamageBonus { get; set; }
    public int TotalDamage { get; set; }
    public List<CreatureTakeDamageResult> VitalityDamage { get; set; } = [];
}
