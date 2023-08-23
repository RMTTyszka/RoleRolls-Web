using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition.Global;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.CreaturesTemplates.Entities
{
    public class CreatureTemplate : Entity
    {
        public CreatureTemplate()
        {
            Attributes = new List<AttributeTemplate>();
            Skills = new List<SkillTemplate>();
            Lifes = new List<LifeTemplate>();
        }
        public CreatureTemplate(CreatureTemplateModel template) : base()
        {
            Name = template.Name;
            TotalAttributePoints = template.TotalAttributePoints;
            TotalSkillsPoints = template.TotalSkillsPoints;
            Attributes = template.Attributes.Select(attribute => new AttributeTemplate(attribute)).ToList();
            Skills = template.Skills.Select(skill => new SkillTemplate(skill.AttributeId, skill)).ToList();
            Lifes = template.Lifes.Select(life => new LifeTemplate(life)).ToList();
        }

        public string Name { get; set; }
        public int MaxAttributePoints => 5;
        // 5 + 4 + 3 + 2 + 2 + 1 = 17
        public int TotalAttributePoints { get; set; }
        // 4
        public int TotalSkillsPoints { get; set; }
        public virtual List<AttributeTemplate> Attributes { get; set; }

        public Creature InstantiateCreature(string name, Guid campaignId, CreatureType type, Guid ownerId)
        {
            var creature = Creature.FromTemplate(this, campaignId);
            creature.OwnerId = ownerId;
            creature.Type = type;
            creature.Name = name;
            return creature;
        }

        public ICollection<SkillTemplate> Skills { get; set; }

        public ICollection<LifeTemplate> Lifes { get; set; }

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
            var skills = Skills.Where(skill => skill.AttributeId == attributeId);
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

        public async Task AddSkill(Guid attributeId, SkillTemplateModel skill, RoleRollsDbContext dbContext)
        {
            var newSkill = new SkillTemplate(attributeId, skill);
            Skills.Add(newSkill);
            await dbContext.SkillTemplates.AddAsync(newSkill);
        }

        public async Task AddMinorSkillAsync(Guid skillId, MinorSkillTemplateModel minorSkill, RoleRollsDbContext dbContext)
        {
            var newMinorSkill = new MinorSkillTemplate(skillId, minorSkill);
            var skill = Skills.First(skill => skill.Id == skillId);
            await skill.AddMinorSkillAsync(newMinorSkill, dbContext);
        }       
        public void UpdateMinorSkill(Guid skillId, Guid minorSkillId, MinorSkillTemplateModel minorSkillModel, RoleRollsDbContext dbContext)
        {
            var skill = Skills.First(skill => skill.Id == skillId);
            var minorSkill = skill.MinorSkills.First(minorSkill => minorSkill.Id == minorSkillId);
            minorSkill.Update(minorSkillModel, dbContext);
        }

        public void RemoveMinorSkill(Guid skillId, Guid minorSkillId, RoleRollsDbContext dbContext)
        {
            var skill = Skills.First(skill => skill.Id == skillId);
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
    }
}
