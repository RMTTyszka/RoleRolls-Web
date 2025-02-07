using System.ComponentModel.DataAnnotations.Schema;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Campaigns.Events.Defenses;
using RoleRollsPocketEdition.Campaigns.Models;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Damages.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Templates.Entities
{
    public class CampaignTemplate : Entity
    {
        public CampaignTemplate()
        {
            Attributes = [];
            Lifes = [];
            Defenses = new List<DefenseTemplate>();
            ItemConfiguration = new ItemConfiguration();
            AttributelessSkills = new List<SkillTemplate>();
            DamageTypes = new List<DamageType>();
        }
        public CampaignTemplate(CampaignTemplateModel template) : base()
        {
            Name = template.Name;
            TotalAttributePoints = template.TotalAttributePoints;
            TotalSkillsPoints = template.TotalSkillsPoints;
            Attributes = template.Attributes.Select(attribute => new AttributeTemplate(attribute)).ToList();
            Lifes = template.Lifes.Select(life => new LifeTemplate(life)).ToList();
            ItemConfiguration = new ItemConfiguration(this, template.ItemConfiguration);
        }

        public CampaignTemplate(CampaignCreateInput campaignModel)
        {
            Name = campaignModel.Name;
            Id = campaignModel.CampaignTemplateId ?? campaignModel.Id;
            ItemConfiguration = new ItemConfiguration(this);
        }

        public string Name { get; set; }
        public bool Default { get; set; }
        public string CreatureTypeTitle { get; set; }
        public string ArchetypeTitle { get; set; }
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

        public Creature InstantiateCreature(string name, Guid campaignId, CreatureCategory category, Guid ownerId)
        {
            var creature = Creature.FromTemplate(this, campaignId, category);
            creature.OwnerId = ownerId;
            creature.Category = category;
            creature.Name = name;
            return creature;
        }

        [NotMapped]
        public List<SkillTemplate> Skills => Attributes.SelectMany(a => a.SkillTemplates).ToList();

        public List<LifeTemplate> Lifes { get; set; }
        public List<DefenseTemplate> Defenses { get; set; }

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
                foreach (var minorSkill in skill.MinorSkills)
                {
                    skill.MinorSkills.Remove(minorSkill);
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
                var skill = attribute.AddSkill(skillModel);
                await dbContext.SkillTemplates.AddAsync(skill);
            }
            else
            {
                AddAttributelessSkill(skillModel);
            }

        }

        private SkillTemplate AddAttributelessSkill(SkillTemplateModel skillModel)
        {
            var skill = new SkillTemplate(Id, skillModel);
            AttributelessSkills.Add(skill);
            return skill;
        }

        public async Task AddMinorSkillAsync(Guid skillId, MinorSkillTemplateModel minorSkill, RoleRollsDbContext dbContext)
        {
            var newMinorSkill = new MinorSkillTemplate(skillId, minorSkill);
            var skill = Skills.Concat(AttributelessSkills).First(skill => skill.Id == skillId);
            await skill.AddMinorSkillAsync(newMinorSkill, dbContext);
        }       
        public void UpdateMinorSkill(Guid skillId, Guid minorSkillId, MinorSkillTemplateModel minorSkillModel, RoleRollsDbContext dbContext)
        {
            var skill = Skills.Concat(AttributelessSkills).First(skill => skill.Id == skillId);
            var minorSkill = skill.MinorSkills.First(minorSkill => minorSkill.Id == minorSkillId);
            minorSkill.Update(minorSkillModel, dbContext);
        }

        public void RemoveMinorSkill(Guid skillId, Guid minorSkillId, RoleRollsDbContext dbContext)
        {
            var skill = Skills.Concat(AttributelessSkills).First(skill => skill.Id == skillId);
            var minorSkill = skill.MinorSkills.First(minorSkill => minorSkill.Id == minorSkillId);
            skill.MinorSkills.Remove(minorSkill);
            dbContext.MinorSkillTemplates.Remove(minorSkill);
        }

        public async Task AddLifeAsync(LifeTemplateModel life, RoleRollsDbContext dbContext)
        {
            var newLife = new LifeTemplate(life);
            Lifes.Add(newLife);
            await dbContext.LifeTemplates.AddAsync(newLife);
        }

        public void RemoveLife(Guid lifeId, RoleRollsDbContext dbContext)
        {
            var life = Lifes.First(life => life.Id == lifeId);
            Lifes.Remove(life);
            dbContext.LifeTemplates.Remove(life);
        }

        public void UpdateLife(Guid lifeId, LifeTemplateModel lifeModel, RoleRollsDbContext dbContext)
        {
            var life = Lifes.First(life => life.Id == lifeId);
            life.Update(lifeModel);
            dbContext.LifeTemplates.Update(life);
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
    }
}
