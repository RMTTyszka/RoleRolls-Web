using System.ComponentModel.DataAnnotations.Schema;
using RoleRollsPocketEdition.Archetypes;
using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Archetypes.Models;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Campaigns.Events.Defenses;
using RoleRollsPocketEdition.Campaigns.Models;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.CreatureTypes.Entities;
using RoleRollsPocketEdition.CreatureTypes.Models;
using RoleRollsPocketEdition.Damages.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Powers.Entities;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Templates.Entities
{
    public class CampaignTemplate : Entity
    {
        public string Name { get; set; }
        public bool Default { get; set; }
        public string? CreatureTypeTitle { get; set; }
        public string? ArchetypeTitle { get; set; }
        public int MaxAttributePoints => 5;
        // 5 + 4 + 3 + 2 + 2 + 1 = 17
        public int TotalAttributePoints { get; set; }
        // 4
        public int TotalSkillsPoints { get; set; }
        public ItemConfiguration ItemConfiguration { get; set; }
        public List<AttributeTemplate> Attributes { get; set; }
        public List<DamageType> DamageTypes { get; set; }
        public List<SkillTemplate> AttributelessSkills { get; set; }
        public List<Campaign> Campaigns { get; set; }
        public ICollection<VitalityTemplate> Vitalities { get; set; }
        public ICollection<DefenseTemplate> Defenses { get; set; }
        public ICollection<CreatureType> CreatureTypes { get; set; }
        public ICollection<PowerTemplate> CombatManeuvers { get; set; } = new List<PowerTemplate>();

        public CampaignTemplate()
        {
            Attributes = [];
            Vitalities = [];
            Defenses = [];
            ItemConfiguration = new ItemConfiguration();
            AttributelessSkills = [];
            DamageTypes = [];
            Archetypes = [];
            CreatureTypes = [];
        }
        public CampaignTemplate(CampaignTemplateModel template) : base()
        {
            Name = template.Name;
            TotalAttributePoints = template.TotalAttributePoints;
            TotalSkillsPoints = template.TotalSkillsPoints;
            Attributes = template.Attributes.Select(attribute => new AttributeTemplate(attribute)).ToList();
            Vitalities = template.Vitalities.Select(vitality => new VitalityTemplate(vitality)).ToList();
            ItemConfiguration = new ItemConfiguration(this, template.ItemConfiguration);
        }

        public CampaignTemplate(CampaignCreateInput campaignModel)
        {
            Name = campaignModel.Name;
            Id = campaignModel.CampaignTemplateId ?? campaignModel.Id;
            ItemConfiguration = new ItemConfiguration(this);
        }
        public Creature InstantiateCreature(string name, Guid id, Guid campaignId, CreatureCategory category, Guid ownerId,
            bool isTemplate)
        {
            var creature = Creature.FromTemplate(this, campaignId, category, isTemplate);
            creature.Id = id;
            creature.OwnerId = ownerId;
            creature.Category = category;
            creature.Name = name;
            return creature;
        }

        [NotMapped]
        public List<SkillTemplate> Skills => Attributes.SelectMany(a => a.SkillTemplates).ToList();

        public List<Archetype> Archetypes { get; set; }


        public async Task AddAttributeAsync(AttributeTemplateModel attribute, RoleRollsDbContext _dbContext)
        {
            var newAttribute = new AttributeTemplate(attribute);
            Attributes.Add(newAttribute);
            await _dbContext.AttributeTemplates.AddAsync(newAttribute);
        }

        public void RemoveAttribute(Guid attributeId, RoleRollsDbContext dbContext)
        {
            var attribute = Attributes.First(attribute => attribute.Id == attributeId);
            Attributes.Remove(attribute);
            var skills = Skills.Where(skill => skill.AttributeTemplateId == attributeId);
            foreach (var skill in skills)
            {
                Skills.Remove(skill);
                foreach (var minorSkill in skill.SpecificSkills)
                {
                    skill.SpecificSkills.Remove(minorSkill);
                    dbContext.MinorSkillTemplates.Remove(minorSkill);
                }
                dbContext.SkillTemplates.Remove(skill);
            }
            dbContext.AttributeTemplates.Remove(attribute);
        }

        public void UpdateAttribute(Guid attributeId, AttributeTemplateModel attributeModel, RoleRollsDbContext dbContext)
        {
            var attribute = Attributes.First(attribute => attribute.Id == attributeId);
            attribute.Update(attributeModel);
            dbContext.AttributeTemplates.Update(attribute);
        }

        public void UpdateSkill(Guid skillId, SkillTemplateModel skillModel, RoleRollsDbContext dbContext)
        {
            var skill = Skills.First(skill => skill.Id == skillId);
            skill.Update(skillModel);
            dbContext.SkillTemplates.Update(skill);
        }

        public void RemoveSkill(Guid skillId, RoleRollsDbContext dbContext)
        {
            var skill = Skills.First(skill => skill.Id == skillId);
            Skills.Remove(skill);
            dbContext.SkillTemplates.Remove(skill);
        }

        public async Task AddSkill(Guid? attributeId, SkillTemplateModel skillModel, RoleRollsDbContext dbContext)
        {
            if (attributeId.HasValue)
            {
                var attribute  = Attributes.First(attribute => attribute.Id == attributeId);
                var skill = await attribute.AddSkill(skillModel, dbContext);
            }
            else
            {
                await AddAttributelessSkill(skillModel, dbContext);
            }

        }

        private async Task<SkillTemplate> AddAttributelessSkill(SkillTemplateModel skillModel, RoleRollsDbContext dbContext)
        {
            var skill = new SkillTemplate(Id, skillModel);
            AttributelessSkills.Add(skill);
            await dbContext.SkillTemplates.AddAsync(skill);
            return skill;
        }

        public async Task AddMinorSkillAsync(Guid skillId, MinorSkillTemplateModel minorSkill, RoleRollsDbContext dbContext)
        {
            var newMinorSkill = new SpecificSkillTemplate(skillId, minorSkill);
            var skill = Skills.Concat(AttributelessSkills).First(skill => skill.Id == skillId);
            await skill.AddMinorSkillAsync(newMinorSkill, dbContext);
        }       
        public void UpdateMinorSkill(Guid skillId, Guid minorSkillId, MinorSkillTemplateModel minorSkillModel, RoleRollsDbContext dbContext)
        {
            var skill = Skills.Concat(AttributelessSkills).First(skill => skill.Id == skillId);
            var minorSkill = skill.SpecificSkills.First(minorSkill => minorSkill.Id == minorSkillId);
            minorSkill.Update(minorSkillModel, dbContext);
        }

        public void RemoveMinorSkill(Guid skillId, Guid minorSkillId, RoleRollsDbContext dbContext)
        {
            var skill = Skills.Concat(AttributelessSkills).First(skill => skill.Id == skillId);
            var minorSkill = skill.SpecificSkills.First(minorSkill => minorSkill.Id == minorSkillId);
            skill.SpecificSkills.Remove(minorSkill);
            dbContext.MinorSkillTemplates.Remove(minorSkill);
        }

        public async Task AddVitalityAsync(VitalityTemplateModel vitality, RoleRollsDbContext dbContext)
        {
            var newVitality = new VitalityTemplate(vitality);
            newVitality.CampaignTemplate = this;
            newVitality.CampaignTemplateId = Id;
            Vitalities.Add(newVitality);
            await dbContext.VitalityTemplates.AddAsync(newVitality);
        }

        public void RemoveVitality(Guid vitalityId, RoleRollsDbContext dbContext)
        {
            var vitality = Vitalities.First(vitality => vitality.Id == vitalityId);
            Vitalities.Remove(vitality);
            dbContext.VitalityTemplates.Remove(vitality);
        }

        public void UpdateVitality(Guid vitalityId, VitalityTemplateModel vitalityModel, RoleRollsDbContext dbContext)
        {
            var vitality = Vitalities.First(vitality => vitality.Id == vitalityId);
            vitality.Update(vitalityModel);
            dbContext.VitalityTemplates.Update(vitality);
        }
        public async Task<DefenseTemplateAdded> AddDefenseAsync(DefenseTemplateModel defense, RoleRollsDbContext dbContext)
        {
            var newDefense = new DefenseTemplate(defense);
            Defenses.Add(newDefense);
            await dbContext.DefenseTemplates.AddAsync(newDefense);
            return DefenseTemplateAdded.FromDefenseTemplate(defense);
        }

        public DefenseTemplateRemoved RemoveDefense(Guid defenseId, RoleRollsDbContext dbContext)
        {
            var defense = Defenses.First(defense => defense.Id == defenseId);
            Defenses.Remove(defense);
            dbContext.DefenseTemplates.Remove(defense);
            return new DefenseTemplateRemoved
            {
                DefenseTemplateId = defenseId
            };
        }

        public DefenseTemplateUpdated UpdateDefense(DefenseTemplateModel defenseModel, RoleRollsDbContext dbContext)
        {
            var defense = Defenses.First(defense => defense.Id == defenseModel.Id);
            defense.Update(defenseModel);
            dbContext.DefenseTemplates.Update(defense);
            return DefenseTemplateUpdated.FromDefenseTemplate(defenseModel);
        }

        public async Task AddDamageTypeAsync(DamageType codeVitality, RoleRollsDbContext context)
        {
            DamageTypes.Add(codeVitality);
            await context.DamageTypes.AddAsync(codeVitality);
        }

        public async Task AddCreatureTypeAsync(CreatureTypeModel creatureTypeModel, RoleRollsDbContext dbContext)
        {
            var creatureType = new CreatureType(creatureTypeModel);
            CreatureTypes.Add(creatureType);
            await dbContext.CreatureTypes.AddAsync(creatureType);
        }

        public async Task UpdateCreatureType(Guid creatureTypeId, CreatureTypeModel creatureTypeModel,
            RoleRollsDbContext dbContext)
        {
            var creatureType = CreatureTypes.First(creatureType => creatureType.Id == creatureTypeId);
            await creatureType.Update(creatureTypeModel, dbContext);
        }

        public async Task AddArchetypeAsync(ArchetypeModel archetypeModel, RoleRollsDbContext dbContext)
        {
            var archetype = new Archetype(archetypeModel);
            Archetypes.Add(archetype);
            await dbContext.Archetypes.AddAsync(archetype);
        }

        public async Task UpdateArchetypeAsync(ArchetypeModel archetypeModel, RoleRollsDbContext dbContext)
        {
            var archetype = Archetypes.First(e => e.Id == archetypeModel.Id);
            await archetype.Update(archetypeModel, dbContext);
        }

        public void RemoveArchetype(Guid archetypeId, RoleRollsDbContext dbContext)
        {
            var entity = Archetypes.First(defense => defense.Id == archetypeId);
            Archetypes.Remove(entity);
            dbContext.Archetypes.Remove(entity);
        }
        public void RemoveCreatureType(Guid creatureTypeId, RoleRollsDbContext dbContext)
        {
            var defense = CreatureTypes.First(defense => defense.Id == creatureTypeId);
            CreatureTypes.Remove(defense);
            dbContext.CreatureTypes.Remove(defense);
        }
    }
}
