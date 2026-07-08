using RoleRollsPocketEdition.Attacks.Models;
using RoleRollsPocketEdition.Campaigns.Repositories;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.Scenes.Services;

namespace RoleRollsPocketEdition.Attacks.Services;

public interface ISpecialAttackService
{
    Task<SpecialAttackResponse> Attack(Guid campaignId, Guid sceneId, Guid attackerId, SpecialAttackInput input);
}

public class SpecialAttackService : ISpecialAttackService, ITransientDependency
{
    private readonly ICreatureRepository _creatureRepository;
    private readonly IScenesService _scenesService;
    private readonly IDiceRoller _diceRoller;

    public SpecialAttackService(
        ICreatureRepository creatureRepository,
        IScenesService scenesService,
        IDiceRoller diceRoller)
    {
        _creatureRepository = creatureRepository;
        _scenesService = scenesService;
        _diceRoller = diceRoller;
    }

    public async Task<SpecialAttackResponse> Attack(
        Guid campaignId,
        Guid sceneId,
        Guid attackerId,
        SpecialAttackInput input)
    {
        var attacker = await LoadCreature(attackerId);
        var target = await LoadCreature(input.TargetId);
        var command = new SpecialAttackCommand
        {
            SpecialSkillId = input.SpecialSkillId,
            DefenseId = input.DefenseId,
            Luck = input.Luck,
            Advantage = input.Advantage
        };

        var attackResult = attacker.SpecialAttack(target, command, _diceRoller);
        await _scenesService.ProcessSpecialAttackAction(sceneId, attackResult);
        return SpecialAttackResponse.From(attackResult);
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
}

public class SpecialAttackCommand
{
    public Guid SpecialSkillId { get; set; }
    public Guid DefenseId { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
}

public class SpecialAttackResult
{
    public Creature Attacker { get; set; } = null!;
    public Creature Target { get; set; } = null!;
    public Guid SpecialSkillId { get; set; }
    public string SpecialSkillName { get; set; } = string.Empty;
    public Guid DefenseId { get; set; }
    public string DefenseName { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string RolledDices { get; set; } = string.Empty;
    public int Difficulty { get; set; }
    public int Complexity { get; set; }
    public int NumberOfSuccesses { get; set; }
    public int NumberOfRollSuccesses { get; set; }
    public int Bonus { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
}
