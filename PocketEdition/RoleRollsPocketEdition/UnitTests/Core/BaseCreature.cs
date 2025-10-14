using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Templates.Dtos;
using RoleRollsPocketEdition.Templates.Entities;
using Attribute = RoleRollsPocketEdition.Creatures.Entities.Attribute;

namespace RoleRollsPocketEdition.UnitTests.Core;

public class BaseCreature
{
    public Creature Creature { get; set; }
    public BaseCreature(CampaignTemplate campaignTemplate, string name)
    {
        Creature = Creature.FromTemplate(campaignTemplate, Guid.Empty, CreatureCategory.Hero, false);
        Creature.Name = name;
        foreach (var attribute in Creature.Attributes)
        {
            attribute.Points = 4;
        }

        foreach (var skill in Creature.Skills)
        {
            foreach (var skillSpecificSkill in skill.SpecificSkills)
            {
                skillSpecificSkill.Points = 3;
            }
        }

        var weapon = new ItemInstance
        {
            Template = new WeaponTemplate
                { Category = WeaponCategory.Medium, DamageType = WeaponDamageType.Bludgeoning },
            Level = 1,
            Id = Guid.NewGuid()
        };

        Creature.AddItemToInventory(weapon);
        Creature.Equip(weapon, EquipableSlot.MainHand);
        
        var armor = new ItemInstance
        {
            Template = new ArmorTemplate()
                { Category = ArmorCategory.Medium },
            Level = 1,
            Id = Guid.NewGuid()
        };
        Creature.AddItemToInventory(armor);
        Creature.Equip(armor, EquipableSlot.Chest);
        
        Creature.FullRestore();
    }

    public BaseCreature WithWeapon(WeaponCategory category, EquipableSlot slot, int level)
    {
        var weapon = new ItemInstance
        {
            Template = new WeaponTemplate
                { Category = category, DamageType = WeaponDamageType.Bludgeoning },
            Level = level
        };
        Creature.AddItemToInventory(weapon);
        Creature.Equip(weapon, slot);
        return this;
    }   
    public BaseCreature WithArmor(ArmorCategory category, int level)
    {
        var armor = new ItemInstance
        {
            Template = new ArmorTemplate()
                { Category = category },
            Level = level
        };
        Creature.AddItemToInventory(armor);
        Creature.Equip(armor, EquipableSlot.Chest);
        return this;
    }

    public BaseCreature WithLevel(int level)
    {
        foreach (var currentLevel in Enumerable.Range(1, level-1))
        {
            Creature.LevelUp();
            foreach (var attribute in Creature.Attributes)
            {
                Creature.AddPointToAttribute(attribute.Id);
            }    
            foreach (var skill in Creature.Skills)
            {
                Creature.AddPointToSkill(skill.Id);
            }      
            foreach (var specificSkill in Creature.SpecificSkills)
            {
                Creature.AddPointToSpecificSkill(specificSkill.Id);
            }
        }
        return this;
    }
}