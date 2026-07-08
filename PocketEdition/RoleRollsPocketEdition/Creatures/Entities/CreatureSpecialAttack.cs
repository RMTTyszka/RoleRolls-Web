using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Rolls.Services;

namespace RoleRollsPocketEdition.Creatures.Entities;

public partial class Creature
{
    public SpecialAttackResult SpecialAttack(Creature target, SpecialAttackCommand input, IDiceRoller diceRoller)
    {
        var specialSkill = new Property(input.SpecialSkillId, PropertyType.MinorSkill);
        var skillValue = GetPropertyValue(new PropertyInput(specialSkill));
        var diceCount = Math.Max(0, skillValue.Total);
        var advantage = Math.Max(0, input.Advantage);
        var bonus = skillValue.Total;
        var complexity = target.DefenseValue(input.DefenseId);

        var roll = ExecuteRollWithValue(
            propertyValue: diceCount,
            propertyBonus: 0,
            advantage: advantage,
            bonus: bonus,
            difficulty: 1,
            complexity: complexity,
            luck: input.Luck,
            diceSides: 20,
            diceRoller: diceRoller,
            property: specialSkill);

        return new SpecialAttackResult
        {
            Attacker = this,
            Target = target,
            SpecialSkillId = input.SpecialSkillId,
            SpecialSkillName = ResolveSpecialSkillName(input.SpecialSkillId),
            DefenseId = input.DefenseId,
            DefenseName = target.ResolveDefenseName(input.DefenseId),
            Success = roll.Success,
            RolledDices = roll.RolledDices,
            Difficulty = roll.Difficulty,
            Complexity = roll.Complexity,
            NumberOfSuccesses = roll.NumberOfSuccesses,
            NumberOfRollSuccesses = roll.NumberOfRollSuccesses,
            Bonus = roll.Bonus,
            Luck = roll.Luck,
            Advantage = roll.Advantage
        };
    }

    private string ResolveSpecialSkillName(Guid specialSkillId)
    {
        return SpecificSkills
            .FirstOrDefault(skill => skill.Id == specialSkillId || skill.SpecificSkillTemplateId == specialSkillId)
            ?.Name ?? "Special Skill";
    }

    private string ResolveDefenseName(Guid defenseId)
    {
        return Defenses
            .FirstOrDefault(defense => defense.Id == defenseId || defense.DefenseTemplateId == defenseId)
            ?.Name ?? "Defense";
    }
}
