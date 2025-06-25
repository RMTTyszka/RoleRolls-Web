using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Rolls.Commands;
using RoleRollsPocketEdition.Rolls.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities;

public partial class Creature
{
    public class EvadeResult
    {
        public Creature Defender { get; set; }
        public Creature Attacker { get; set; }
        public int EvadedHits { get; set; }
        public int RemainingHits { get; set; }
        public Roll Roll { get; set; }
    }
    public AttackResult Evade(Creature attacker, AttackCommand input)
{
    var defenseId = input.GetDefenseId;
    var defenseProperty = new Property(defenseId);
    var defenseValue = GetPropertyValue(new PropertyInput(defenseProperty));
    var advantage = Math.Max(input.Advantage, GetTotalBonus(BonusApplication.Evasion, BonusType.Advantage, null));

    var weapon = attacker.Equipment.GetItem(input.WeaponSlot) ?? new ItemInstance
    {
        Template = new WeaponTemplate { Category = WeaponCategory.Medium }
    };
    var weaponTemplate = (WeaponTemplate)weapon.Template;
    var gripStats = GripTypeExtensions.Stats[attacker.Equipment.GripType];
    var difficulty = WeaponDefinition.HitDifficulty(weaponTemplate.Category);

    var rollCommand = new RollDiceCommand(
        defenseValue.Value,
        advantage,
        defenseValue.Bonus,
        difficulty, 
        difficulty,
        [],
        input.Luck
    );

    var roll = new Roll();
    roll.Process(rollCommand);

    var numberOfFails = roll.NumberOfDices - roll.NumberOfRollSuccesses;

    var damages = new List<DamageRollResult>();
    for (int i = 0; i < numberOfFails; i++)
    {
        var damageProperty = attacker.GetPropertyValue(new PropertyInput(
            input.ItemConfiguration.GetWeaponDamageProperty(weaponTemplate.Category),
            input.DamageAttribute
        ));
        var damage = attacker.RollDamage(weapon, damageProperty, gripStats);
        damage.ReducedDamage -= GetBasicBlock();
        damage.ReducedDamage = Math.Max(1, damage.ReducedDamage);
        damages.Add(damage);

        var result = TakeDamage(input.GetVitalityId, damage.TotalDamage);
        if (result.ExcessDamage > 0)
        {
            TakeDamage(input.GetSecondVitalityId, result.ExcessDamage);
        }
    }

    return new AttackResult
    {
        Attacker = attacker,
        Target = this,
        Weapon = weapon,
        TotalDamage = damages.Sum(d => d.ReducedDamage),
        Success = numberOfFails > 0
    };
}


}