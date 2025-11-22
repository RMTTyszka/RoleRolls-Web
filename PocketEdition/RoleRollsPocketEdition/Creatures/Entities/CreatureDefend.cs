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

    public AttackResult Evade(Creature attacker, AttackCommand input, IDiceRoller diceRoller)
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
        var gripStats = GripTypeDefinition.Stats[attacker.Equipment.GripType];

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
        defenseAdvantage += ResolveWeaponVsArmorAdvantage(weapon, Equipment.ArmorCategory);
        var luck = input.Luck;
        luck += ResolveWeaponVsArmorLuck(weapon, Equipment.ArmorCategory);
        var chestArmor = Equipment.Chest;
        var armorCategory = chestArmor?.ArmorTemplate?.Category ?? ArmorCategory.None;
        var armorDefenseBonus = chestArmor?.GetDefenseBonus1() ?? ArmorDefinition.DefenseBonus1(armorCategory);
        var armorBonus = chestArmor?.GetBonus ?? 0;
        var evadeRollCommand = new RollDiceCommand(
            defenseValue.Value,
            defenseAdvantage,
            defenseValue.Bonus + defenseValue.Value + armorBonus + armorDefenseBonus,
            evadeComplexity,
            evadeComplexity,
            [],
            luck
        );
        var evadeRoll = new Roll();
        evadeRoll.Process(evadeRollCommand, diceRoller, 20);

        var remainingSuccesses = Math.Max(0, attackSuccesses - evadeRoll.NumberOfSuccesses);

        var hitDifficulty = gripStats.AttackDifficult;
        var numberOfHits = remainingSuccesses / hitDifficulty;

        var damageProperty = attacker.GetPropertyValue(new PropertyInput(
            input.ItemConfiguration.GetWeaponDamageProperty(weaponCategory),
            input.DamageAttribute
        ));

        var damages = new List<DamageRollResult>();
        for (int i = 0; i < numberOfHits; i++)
        {
            var property = input.ItemConfiguration.BlockProperty;
            var propertyValue = attacker.GetPropertyValue(new PropertyInput(property, input.BlockProperty));
            var damage = attacker.RollDamage(weapon, damageProperty, gripStats, diceRoller);
            damage.ReducedDamage -= GetBasicBlock(propertyValue);
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
            Success = numberOfHits <= 0
        };
    }
    public int GetBasicBlock(PropertyValue blockProperty)
    {
        var armor = Equipment.Chest;
        var armorCategory = ArmorCategory.None;
        var armorLevelBonus = 0;
        if (armor is not null)
        {
            var armorTemplate = armor.ArmorTemplate;
            armorCategory = armorTemplate.Category;
            armorLevelBonus = armor.GetBonus;
        }

        var blockLevelModifier = ArmorDefinition.DamageReductionByLevel(armorCategory);
        var baseBlock = ArmorDefinition.BaseDamageReduction(armorCategory);
        var total = blockLevelModifier * armorLevelBonus + baseBlock + blockProperty.Value;
        Console.WriteLine($"BLOCK: {total}, LEVEL: {Level}, ARMOR: {armorCategory}, BONUS: {armorLevelBonus}, BLOCK_LEVEL_MODIFIER: {blockLevelModifier}, BASE_BLOCK: {baseBlock}, BLOCK_PROPERTY: {blockProperty.Value}");
        return total;
    }

    private int GetEvasionLuck()
    {
        var armorCategory = Equipment.ArmorCategory;
        var evasion = ArmorDefinition.BaseLuck(armorCategory);
        return evasion;
    }
}


