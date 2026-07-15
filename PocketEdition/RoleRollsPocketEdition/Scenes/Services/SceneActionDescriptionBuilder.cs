using RoleRollsPocketEdition.Attacks.Services;

namespace RoleRollsPocketEdition.Scenes.Services;

public static class SceneActionDescriptionBuilder
{
    public static string BuildBasicAttackDescription(BasicAttackResult attackResult, string statusDescription)
    {
        var weaponName = string.IsNullOrWhiteSpace(attackResult.Weapon?.Name)
            ? "weapon"
            : attackResult.Weapon.Name;
        var baseDescription =
            $"{attackResult.Attacker.Name} attacked {attackResult.Target.Name} with {weaponName} and caused {attackResult.TotalDamage} damage";

        return string.IsNullOrWhiteSpace(statusDescription)
            ? baseDescription
            : $"{baseDescription} | {statusDescription}";
    }

    public static string BuildSpecialAttackDescription(SpecialAttackResult attackResult)
    {
        var outcome = attackResult.Success ? "succeeded" : "failed";
        return
            $"{attackResult.Attacker.Name} used {attackResult.SpecialSkillName} against {attackResult.Target.Name} ({attackResult.DefenseName}) and {outcome}";
    }

    public static string BuildEvadeDescription(EvadeResult evadeResult, string statusDescription)
    {
        var weaponName = string.IsNullOrWhiteSpace(evadeResult.Weapon?.Name)
            ? "weapon"
            : evadeResult.Weapon.Name;
        var baseDescription =
            $"{evadeResult.Defender.Name} resolved Evasion against {evadeResult.Attacker.Name} with {weaponName}: {evadeResult.NumberOfHits} hits and {evadeResult.TotalDamage} damage";

        return string.IsNullOrWhiteSpace(statusDescription)
            ? baseDescription
            : $"{baseDescription} | {statusDescription}";
    }
}
