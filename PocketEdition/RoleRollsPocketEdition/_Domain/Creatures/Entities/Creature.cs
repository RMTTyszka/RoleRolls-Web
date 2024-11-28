using System.Data;
using RoleRollsPocketEdition._Application.Attacks.Services;
using RoleRollsPocketEdition._Application.Creatures;
using RoleRollsPocketEdition._Application.Creatures.Models;
using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition._Domain.Campaigns.Entities;
using RoleRollsPocketEdition._Domain.Creatures.Models;
using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;
using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition._Domain.Itens.Configurations;
using RoleRollsPocketEdition._Domain.Itens.Templates;
using RoleRollsPocketEdition._Domain.Rolls.Entities;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Domain.Creatures.Entities
{
    public class Creature : Entity
    {
        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<Skill> Skills { get; set; }

        public ICollection<Life> Lifes { get; set; }
        public ICollection<Defense> Defenses { get; set; }
        public ICollection<CreaturePower> Powers { get; set; }

        public Guid CampaignId { get; set; }
        public Guid CreatureTemplateId { get; set; }
        public Guid OwnerId { get; set; }

        public string Name { get; set; }
        public int Level { get; set; }

        public CreatureType Type { get; set; }
        public Equipment Equipment { get; set; }
        public Inventory Inventory { get; set; }
        public Creature()
        {
            Attributes = new List<Attribute>();
            Skills = new List<Skill>();
            Lifes = new List<Life>();
            Defenses = new List<Defense>();
            Equipment = new Equipment();
            Inventory = new Inventory();
            Level = 1;
        }

        public int DefenseValue(Guid id)
        {
            var defense = Defenses.Where(x => x.Id == id).FirstOrDefault();
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
                    var value = Attributes.First(at => at.Id == propertyId).Value;
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
                    attribute = Attributes.FirstOrDefault(at => at.Id == propertyId);
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
                    minorSkill = Skills.SelectMany(skill => skill.MinorSkills).First(minorSkill => minorSkill.Id == propertyId);
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
        public static Creature FromTemplate(CreatureTemplate template, Guid campaignId, CreatureType creatureType) 
        {
            var attributes = template.Attributes.Select(attribute => new Attribute(attribute)).ToList();
            var creature = new Creature
            {
                Attributes = attributes,
                Skills = template.Skills.Select(skill => new Skill(skill, attributes.First(attribute => attribute.AttributeTemplateId == skill.AttributeId))).ToList(),
                Lifes = template.Lifes.Select(life => new Life(life)).ToList(),
                CampaignId = campaignId,
                CreatureTemplateId = template.Id,
                Type = creatureType
            };
            foreach (var life in creature.Lifes)
            {
                life.CalculateMaxValue(creature);
                life.Value = life.MaxValue;
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
                    var success = value + roll.Bonus > roll.Dificulty;
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
            if (roll.Complexity.HasValue)
            {
                result.Success = result.Successes > roll.Complexity;
            }
            result.Rolls = rolls;
            result.SuccessTimes = result.Successes / roll.Complexity.GetValueOrDefault(1);
            return result;
        }

        public void ProcessLifes()
        {
            foreach (var life in Lifes)
            {
                life.CalculateMaxValue(this);
            }
        }

        public SceneAction TakeDamage(Guid lifeId, int value)
        {
            var life = Lifes.First(life => life.Id == lifeId);
            life.Value -= value;
            return new SceneAction
            {
                Description = $"{Name} took {value} of {life.Name} damage",
                ActorType = Type switch
                {
                    CreatureType.Hero => ActionActorType.Hero,
                    CreatureType.Monster => ActionActorType.Monster,
                    _ => throw new ArgumentOutOfRangeException()
                },
                Id = Guid.NewGuid(),
                ActorId = Id,
            };
        }

        public SceneAction Heal(Guid lifeId, int value)
        {
            var life = Lifes.First(life => life.Id == lifeId);
            life.Value += value;
            life.Value = Math.Min(life.Value, life.MaxValue);
            return new SceneAction
            {
                Description = $"{Name} healed {value} of {life.Name}",
                ActorType = Type switch
                {
                    CreatureType.Hero => ActionActorType.Hero,
                    CreatureType.Monster => ActionActorType.Monster,
                    _ => throw new ArgumentOutOfRangeException()
                },
                Id = Guid.NewGuid(),
                ActorId = Id
            };
        }

        public async Task AddDefenseAsync(Defense defense, RoleRollsDbContext dbContext)
        {
           Defenses.Add(defense);
           await dbContext.AddAsync(defense);
        }

        public async Task UpdateDefense(DefenseTemplateModel defenseTemplateModel, RoleRollsDbContext dbContext)
        {
            var defense = Defenses.First(defense => defense.DefenseTemplateId == defenseTemplateModel.Id);
            defense.Update(defenseTemplateModel);
            dbContext.Update(defense);
        }

        public void FullRestore()
        {
            ProcessLifes();
            foreach (var life in Lifes)
            {
                life.Value = life.MaxValue;
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
            var weaponTemplate = weapon.Template as WeaponTemplate;
            var hitPropertyId = input.ItemConfiguration.GetWeaponHitProperty(weaponTemplate.Category);
            var damagePropertyId = input.ItemConfiguration.GetWeaponDamageProperty(weaponTemplate.Category);
            var hitProperty = GetPropertyValue(null, hitPropertyId);
            var damageProperty = GetPropertyValue(null, damagePropertyId);
            var defenseValue = target.DefenseValue(input.ItemConfiguration.ArmorDefenseId.Value);
            var armorTemplate = (target.Equipment.Chest?.Template as ArmorTemplate)?.Category ?? ArmorCategory.None;
            var totalHit = hitProperty.Bonus + WeaponDefinition.HitBonus(weaponTemplate.Category);
            var totalDefense = defenseValue + ArmorDefinition.DefenseBonus(armorTemplate);
            var roll = Roll(new RollCheck
            {
                Bonus = totalHit,
                Complexity = defenseValue,
                Dificulty = totalDefense
            }, hitProperty.Value);
            if (roll.Success)
            {
                var totalDamage = 0;
                for (var i = 0; i < roll.SuccessTimes; i++)
                {
                    var damage = RollDamage(weapon, damageProperty);
                }
            }

            return new AttackResult
            {

            };

        }

        private int RollDamage(ItemInstance weapon, PropertyValue damageProperty)
        {
            var random = new Random();
            var weaponTemplate = weapon.Template as WeaponTemplate;
            var damageMaxValue = WeaponDefinition.DamageFlatBonus(weaponTemplate.Category);
            var damageValue = random.Next(0, 21);
        }
    }
   
}