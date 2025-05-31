using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.DefaultUniverses.Global.CombatManeuvers;
using RoleRollsPocketEdition.Powers.Entities;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.CombatManeuvers;

public static class LandOfHeroesCombatManeuvers
{
    public static readonly List<PowerTemplate> Maneuvers = [
        GlobalCombatManeuvers.OpenShot, 
        GlobalCombatManeuvers.FullAttack, 
        GlobalCombatManeuvers.PartialAttack,
        GlobalCombatManeuvers.CautiousAttack,
        GlobalCombatManeuvers.FullCover,
    ];
}