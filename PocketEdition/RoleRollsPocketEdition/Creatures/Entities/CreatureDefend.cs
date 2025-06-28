using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Rolls.Commands;
using RoleRollsPocketEdition.Rolls.Entities;
using RoleRollsPocketEdition.Rolls.Services;

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

    public AttackResult Evade(Creature attacker, AttackCommand input, IDiceRoller dice)
    {
        var weapon = attacker.Equipment.GetItem(input.WeaponSlot) ?? new ItemInstance
        {
            Template = new WeaponTemplate
            {
                Category = WeaponCategory.Medium,
                DamageType = WeaponDamageType.Bludgeoning
            }
        };
        var weaponTemplate = (WeaponTemplate)weapon.Template;
        var weaponCategory = weaponTemplate.Category;
        var gripStats = GripTypeExtensions.Stats[attacker.Equipment.GripType];

        var hitProperty = input.ItemConfiguration.GetWeaponHitProperty(weaponCategory);
        var hitValue = attacker.GetPropertyValue(new PropertyInput(hitProperty, input.HitAttribute));
        var totalHitBonus = hitValue.Bonus + gripStats.Hit +
                            attacker.GetTotalBonus(BonusApplication.Hit, BonusType.Buff, null);
        var attackSuccesses = hitValue.Value;
        var evadeComplexity = 10 + totalHitBonus;

        var defenseProperty = new Property(input.GetDefenseId);
        var defenseValue = GetPropertyValue(new PropertyInput(defenseProperty));
        var defenseAdvantage =
            Math.Max(input.Advantage, GetTotalBonus(BonusApplication.Evasion, BonusType.Advantage, null));

        var evadeRollCommand = new RollDiceCommand(
            defenseValue.Value,
            defenseAdvantage,
            defenseValue.Bonus,
            evadeComplexity,
            evadeComplexity,
            [],
            input.Luck
        );
        var evadeRoll = new Roll();
        evadeRoll.Process(evadeRollCommand);

        // 3. Subtrair sucessos da evasão dos sucessos do atacante
        var remainingSuccesses = Math.Max(0, attackSuccesses - evadeRoll.NumberOfRollSuccesses);

        // 4. Comparar com dificuldade da arma
        var hitDifficulty = WeaponDefinition.HitDifficulty(weaponCategory);
        var numberOfHits = remainingSuccesses / hitDifficulty;

        // 5. Rolar dano para cada hit
        var damageProperty = attacker.GetPropertyValue(new PropertyInput(
            input.ItemConfiguration.GetWeaponDamageProperty(weaponCategory),
            input.DamageAttribute
        ));

        var damages = new List<DamageRollResult>();
        for (int i = 0; i < numberOfHits; i++)
        {
            var damage = attacker.RollDamage(weapon, damageProperty, gripStats);
            damage.ReducedDamage -= GetBasicBlock();
            damage.ReducedDamage = Math.Max(1, damage.ReducedDamage);
            damages.Add(damage);

            var result = TakeDamage(input.GetVitalityId, damage.TotalDamage);
            if (result.ExcessDamage > 0)
                TakeDamage(input.GetSecondVitalityId, result.ExcessDamage);
        }

        return new AttackResult
        {
            Attacker = attacker,
            Target = this,
            Weapon = weapon,
            TotalDamage = damages.Sum(d => d.ReducedDamage),
            Success = numberOfHits > 0
        };
    }
}