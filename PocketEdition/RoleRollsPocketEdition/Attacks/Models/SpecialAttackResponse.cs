using RoleRollsPocketEdition.Attacks.Services;

namespace RoleRollsPocketEdition.Attacks.Models;

public class SpecialAttackResponse
{
    public Guid AttackerId { get; set; }
    public Guid TargetId { get; set; }
    public bool Success { get; set; }
    public Guid DefenseId { get; set; }
    public string DefenseName { get; set; } = string.Empty;
    public Guid SpecialSkillId { get; set; }
    public string SpecialSkillName { get; set; } = string.Empty;
    public int Complexity { get; set; }
    public int Difficulty { get; set; }
    public int NumberOfSuccesses { get; set; }
    public int NumberOfRollSuccesses { get; set; }
    public int Bonus { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
    public string RolledDices { get; set; } = string.Empty;

    public static SpecialAttackResponse From(SpecialAttackResult result)
    {
        return new SpecialAttackResponse
        {
            AttackerId = result.Attacker.Id,
            TargetId = result.Target.Id,
            Success = result.Success,
            DefenseId = result.DefenseId,
            DefenseName = result.DefenseName,
            SpecialSkillId = result.SpecialSkillId,
            SpecialSkillName = result.SpecialSkillName,
            Complexity = result.Complexity,
            Difficulty = result.Difficulty,
            NumberOfSuccesses = result.NumberOfSuccesses,
            NumberOfRollSuccesses = result.NumberOfRollSuccesses,
            Bonus = result.Bonus,
            Luck = result.Luck,
            Advantage = result.Advantage,
            RolledDices = result.RolledDices
        };
    }
}
