using System.Data;
using RoleRollsPocketEdition.Archetypes;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.CreatureTypes.Entities;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Rolls.Entities;
using RoleRollsPocketEdition.Scenes.Entities;
using RoleRollsPocketEdition.Templates.Dtos;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class Creature : Entity
    {
        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<Skill> Skills { get; set; }

        public ICollection<Vitality> Vitalities { get; set; }
        public ICollection<Defense> Defenses { get; set; }
        public ICollection<CreaturePower> Powers { get; set; }

        public Guid CampaignId { get; set; }
        public Guid CreatureTemplateId { get; set; }
        public Guid OwnerId { get; set; }

        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsTemplate { get; set; }

        public CreatureCategory Category { get; set; }
        public Equipment Equipment { get; set; }
        public Inventory Inventory { get; set; }
        public CreatureType? CreatureType { get; set; }
        public Archetype? Archetype { get; set; }
        public Creature()
        {
            Attributes = new List<Attribute>();
            Skills = new List<Skill>();
            Vitalities = new List<Vitality>();
            Defenses = new List<Defense>();
            Equipment = new Equipment();
            Inventory = new Inventory();
            Level = 1;
        }

        public int DefenseValue(Guid id)
        {
            var defense = Defenses.Where(x => x.Id == id || x.DefenseTemplateId == id).First();
            var value = ApplyFormula(defense.Formula);
            return value;
        }



        public PropertyValue GetPropertyValue(RollPropertyType? propertyType, Guid propertyId)
        {
            Attribute? attribute;
            Skill? skill;
            MinorSkill? minorSkill;
            int rollBonus;
            var result = new PropertyValue();
            switch (propertyType)
            {
                case RollPropertyType.Attribute:
                    var value = Attributes.First(at => at.Id == propertyId || at.AttributeTemplateId == propertyId).Value;
                    result.Value = value;
                    break;
                case RollPropertyType.Skill:
                    skill = Skills.First(sk => sk.Id == propertyId);
                    attribute = Attributes.First(attribute => attribute.Id == skill.AttributeId);
                    result.Value = attribute.Value;
                    result.Bonus = 0;
                    break;
                case RollPropertyType.MinorSkill:
                    minorSkill = Skills.SelectMany(skill => skill.MinorSkills).First(minorSkill => minorSkill.Id == propertyId);
                    skill = Skills.First(sk => sk.Id == minorSkill.SkillId);
                    attribute = Attributes.First(at => at.Id == skill.AttributeId);
                    rollBonus = minorSkill.Points;
                    result.Value = attribute.Value;
                    result.Bonus = rollBonus;
                    break;
                default:
                    attribute = Attributes.FirstOrDefault(at => at.Id == propertyId || at.AttributeTemplateId == propertyId);
                    if (attribute != null)
                    {
                        result.Value = attribute.Value;
                    }         
                    skill = Skills.FirstOrDefault(at => at.Id == propertyId);
                    if (skill != null)
                    {
                        result.Value = attribute.Value;
                        result.Bonus = 0;
                    }      
                    minorSkill = Skills.SelectMany(skill => skill.MinorSkills).FirstOrDefault(minorSkill => minorSkill.Id == propertyId);
                    if (minorSkill != null)
                    {
                        skill = Skills.First(sk => sk.Id == minorSkill.SkillId);
                        attribute = Attributes.First(at => at.Id == skill.AttributeId);
                        rollBonus = minorSkill.Points;
                        result.Value = attribute.Value;
                        result.Bonus = rollBonus;
                    }
                    break;
            }
            return result;;
        }
        public static Creature FromTemplate(CampaignTemplate template, Guid campaignId, CreatureCategory creatureCategory, bool isTemplate) 
        {
            var attributes = template.Attributes.Select(attribute => new Attribute(attribute)).ToList();
            var creature = new Creature
            {
                Attributes = attributes,
                Skills = template.Skills.Select(skill => new Skill(skill, attributes.First(attribute => attribute.AttributeTemplateId == skill.AttributeTemplateId))).ToList(),
                Vitalities = template.Vitalities.Select(vitality => new Vitality(vitality)).ToList(),
                Defenses = template.Defenses.Select(Defense.FromTemplate).ToList(),
                CampaignId = campaignId,
                CreatureTemplateId = template.Id,
                Category = creatureCategory,
                IsTemplate = isTemplate,
            };
            foreach (var vitality in creature.Vitalities)
            {
                vitality.CalculateMaxValue(creature);
                vitality.Value = vitality.MaxValue;
            }

            return creature;
        }

        public CreatureUpdateValidationResult Update(CreatureModel creatureModel)
        {
            if (Valid(creatureModel))
            {
                Name = creatureModel.Name;
                foreach (var attribute in Attributes)
                {
                    var updatedAttribute = creatureModel.Attributes.First(attr => attr.AttributeTemplateId == attribute.AttributeTemplateId);
                    attribute.Update(updatedAttribute);
                }           
                foreach (var skill in Skills)
                {
                    var updatedSkill= creatureModel.Skills.First(sk => sk.SkillTemplateId == skill.SkillTemplateId);
                    var result = skill.Update(updatedSkill);
                    if (result.Validation == CreatureUpdateValidation.Ok)
                    {
                        continue;
                    }

                    return result;
                }
                
                return new CreatureUpdateValidationResult(CreatureUpdateValidation.Ok, null);
            }
            return new CreatureUpdateValidationResult(CreatureUpdateValidation.InvalidModel, null);
        }

        private bool Valid(CreatureModel creatureModel)
        {
            var hasDifferentArchetype = Archetype is not null && creatureModel.Archetype is not null && Archetype.Id != creatureModel.Archetype.Id;
            var hasDifferentCreatureType = CreatureType is not null && creatureModel.CreatureType is not null && CreatureType.Id != creatureModel.CreatureType.Id;

            if (hasDifferentArchetype || hasDifferentCreatureType)
            {
                return false;
            }
            return true;
        }

        private RollsResult Roll(RollCheck roll, int level)
        {
            var random = new Random();
            var result = new RollsResult();
            var rolls = Enumerable.Range(0, level).Select((int _) =>
            {
                var value = random.Next(0, 21);
                if (roll.Dificulty.HasValue) 
                {
                    var success = value + roll.Bonus > roll.Complexity;
                    if (success)
                    {
                        result.Successes += 1;
                        if (value == 20)
                        {
                            result.CriticalSuccesses += 1;
                        }
                    }
                    else 
                    {
                        result.Misses += 1;
                        if (value == 1)
                        {
                            result.CriticalMisses += 1;
                        }
                    }
                }


                return value;
            }).ToList();
            if (roll.Dificulty.HasValue)
            {
                result.Success = result.Successes > roll.Dificulty;
            }
            result.Rolls = rolls;
            result.SuccessTimes = result.Successes / roll.Complexity.GetValueOrDefault(1);
            return result;
        }

        public CreatureTakeDamageResult TakeDamage(Guid vitalityId, int value)
        {
            var vitality = Vitalities.First(vitality => vitality.Id == vitalityId);
            vitality.Value -= value;
            return new CreatureTakeDamageResult
            {
                Name = Name,
                Value = value,
                Vitality = vitality.Name,
                ActorId = Id
            };

        }        


        public int GetBasicBlock()
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

            var blockLevelModifier = ArmorDefinition.BlockLevelModifier(armorCategory);
            var baseBlock = ArmorDefinition.BaseBlock(armorCategory);
            return blockLevelModifier * armorLevelBonus + baseBlock;
        }

        public SceneAction Heal(Guid vitalityId, int value)
        {
            var vitality = Vitalities.First(vitality => vitality.Id == vitalityId);
            vitality.Value += value;
            vitality.Value = Math.Min(vitality.Value, vitality.MaxValue);
            return new SceneAction
            {
                Description = $"{Name} healed {value} of {vitality.Name}",
                ActorType = Category switch
                {
                    CreatureCategory.Hero => ActionActorType.Hero,
                    CreatureCategory.Monster => ActionActorType.Monster,
                    _ => throw new ArgumentOutOfRangeException()
                },
                Id = Guid.NewGuid(),
                ActorId = Id
            };
        }

        public void FullRestore()
        {
            foreach (var vitality in Vitalities)
            {
                vitality.Value = vitality.MaxValue;
            }
        }

        public int ApplyFormula(string formula)
        {
            var replacesFormula = Attributes.Aggregate(formula,
                (formula, attribute) => formula.Replace(attribute.Name, attribute.TotalValue.ToString()));
     
            replacesFormula = Skills.Aggregate(replacesFormula,
                (formula, skill) => replacesFormula.Replace(skill.Name, GetPropertyValue(RollPropertyType.Skill, skill.Id).ToString()));     
            replacesFormula = MinorSkills.Aggregate(replacesFormula,
                (formula, minorSKill) => replacesFormula.Replace(minorSKill.Name, GetPropertyValue(RollPropertyType.MinorSkill, minorSKill.Id).ToString()));
            DataTable dt = new DataTable();
            var result = dt.Compute(replacesFormula,"");

            if (int.TryParse(result.ToString(), out var value))
            {
                return value;
            }

            return 0;
        }

        public List<MinorSkill> MinorSkills => Skills.SelectMany(s => s.MinorSkills).ToList();

        public void AddItemToInventory(ItemInstance? item)
        {
            Inventory.AddItem(item);
        }
        public async Task RemoveItem(ItemInstance? item)
        {
            Inventory.Remove(item);
        }

        public void Destroy(ItemInstance? item)
        {
            Inventory.Remove(item);
        }

        public async Task Equip(ItemInstance item, EquipableSlot slot)
        {
            Equipment.Equip(item, slot);
        }

        public void Unequip(EquipableSlot slot)
        { 
            Equipment.Unequip(slot);
        }

        public AttackResult Attack(Creature target, AttackCommand input)
        {
            var weapon = Equipment.GetItem(input.WeaponSlot);
            var weaponTemplate = weapon?.Template as WeaponTemplate;
            var weaponCategory = weaponTemplate?.Category ?? WeaponCategory.Light;
            var hitPropertyId = input.ItemConfiguration.GetWeaponHitProperty(weaponCategory);
            var damagePropertyId = input.ItemConfiguration.GetWeaponDamageProperty(weaponCategory);
            var hitProperty = GetPropertyValue(null, hitPropertyId);
            var damageProperty = GetPropertyValue(null, damagePropertyId);
            var defenseId = input.GetDefenseId;
            var defenseValue = target.DefenseValue(defenseId);
            var armorTemplate = (target.Equipment.Chest?.Template as ArmorTemplate)?.Category ?? ArmorCategory.None;
            var totalHit = hitProperty.Bonus + WeaponDefinition.HitBonus(weaponCategory);
            var totalDefense = defenseValue + ArmorDefinition.DefenseBonus(armorTemplate);
            var roll = Roll(new RollCheck
            {
                Bonus = totalHit,
                Complexity = WeaponDefinition.HitDifficulty(weaponCategory),
                Dificulty = totalDefense
            }, hitProperty.Value);
            if (roll.Success)
            {
                var damages = new List<DamageRollResult>();
                for (var i = 0; i < roll.SuccessTimes; i++)
                {
                    var damage = RollDamage(weapon, damageProperty);
                    var block = target.GetBasicBlock();
                    damage.ReducedDamage -= block;
                    damage.ReducedDamage = Math.Max(0, damage.ReducedDamage);
                    damages.Add(damage);
                    target.TakeDamage(input.GetVitalityId, damage.TotalDamage);
                }
                return new AttackResult
                {
                    Attacker = this,
                    Target = target,
                    TotalDamage = damages.Sum(d => d.ReducedDamage),
                    Weapon = weapon,
                    Success = true
                };
            }

            return new AttackResult
            {
                Attacker = this,
                Target = target,
                Weapon = weapon,
                Success = false
            };


        }

        private DamageRollResult RollDamage(ItemInstance weapon, PropertyValue damageProperty)
        {
            var result = new DamageRollResult();
            var random = new Random();
            var weaponTemplate = weapon.Template as WeaponTemplate;
            var maxValue = WeaponDefinition.MaxDamage(weaponTemplate.Category);
            var flatBonus = WeaponDefinition.BaseDamageBonus(weaponTemplate.Category);
            var bonusModifier = WeaponDefinition.DamageBonusModifier(weaponTemplate.Category);
            var damage = random.Next(0, maxValue);
            result.DiceValue = damage;
            result.BonusModifier = bonusModifier;
            result.FlatBonus = flatBonus;
            damage += flatBonus;
            damage += weapon.Level * bonusModifier;
            damage += damageProperty.Bonus * bonusModifier;
            result.TotalDamage = damage;
            result.ReducedDamage = damage;
            // TODO any extra damage * levelModifier;
            return result;
        }
    }
   
}