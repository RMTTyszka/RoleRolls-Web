using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition.CreaturesTemplates.Entities;
using RoleRollsPocketEdition.Global;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Rolls.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class Creature : Entity
    {
        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<Skill> Skills { get; set; }

        public ICollection<Life> Lifes { get; set; }
        public ICollection<Defense> Defenses { get; set; }

        public Guid CampaignId { get; set; }
        public Guid CreatureTemplateId { get; set; }
        public Guid OwnerId { get; set; }

        public string Name { get; set; }

        public CreatureType Type { get; set; }
        public Creature()
        {
            Attributes = new List<Attribute>();
            Skills = new List<Skill>();
            Lifes = new List<Life>();
        }
        public (int propertyValue, int rollBonus) GetPropertyValue(RollPropertyType propertyType, Guid propertyId)
        {
            switch (propertyType)
            {
                case RollPropertyType.Attribute:
                    return (Attributes.First(attribute => attribute.Id == propertyId).Value, 0);
                case RollPropertyType.Skill:
                    return (Skills.First(skill => skill.Id == propertyId).Value, 0);
                case RollPropertyType.MinorSkill:
                    var minorSkill = Skills.SelectMany(skill => skill.MinorSkills).First(minorSkill => minorSkill.Id == propertyId);
                    var skill = Skills.First(skill => skill.Id == minorSkill.SkillId);
                    var attribute = Attributes.First(attribute => attribute.Id == skill.AttributeId);
                    var rollBonus = minorSkill.Points;
                    return (attribute.Value, rollBonus);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public static Creature FromTemplate(CreatureTemplate template, Guid campaignId) 
        {
            var attributes = template.Attributes.Select(attribute => new Attribute(attribute)).ToList();
            var creature = new Creature
            {
                Attributes = attributes,
                Skills = template.Skills.Select(skill => new Skill(skill, attributes.First(attribute => attribute.AttributeTemplateId == skill.AttributeId))).ToList(),
                Lifes = template.Lifes.Select(life => new Life(life)).ToList(),
                CampaignId = campaignId,
                CreatureTemplateId = template.Id,
            };
            foreach (var life in creature.Lifes)
            {
                life.CalculateMaxValue(creature);
                life.Value = life.MaxValue;
            }

            return creature;
        }

        public RollsResult RollSkill(RollCheck roll) 
        {
            var attributeId= Skills.FirstOrDefault(skill => skill.Id == roll.SkillId)?.AttributeId;
            if (attributeId is null) 
            {
                return new RollsResult
                {
                    Success = false
                };
            }

            roll.AttributeId = attributeId.Value;
            var result = RollAttribute(roll);
            return result;
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

        public RollsResult RollAttribute(RollCheck roll) 
        {
            var attribute = Attributes.FirstOrDefault(attribute => attribute.Id == roll.AttributeId);
            if (attribute is null)
            {
                return new RollsResult
                {
                    Success = false
                };
            }
            return Roll(roll, attribute.Value);
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
            return result;
        }

        public void ProcessLifes()
        {
            foreach (var life in Lifes)
            {
                life.CalculateMaxValue(this);
            }
        }

        public void TakeDamage(Guid lifeId, int value)
        {
            var life = Lifes.First(life => life.Id == lifeId);
            life.Value -= value;
        }

        public void Heal(Guid lifeId, int value)
        {
            var life = Lifes.First(life => life.Id == lifeId);
            life.Value += value;
            life.Value = Math.Min(life.Value, life.MaxValue);
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
    }
   
}