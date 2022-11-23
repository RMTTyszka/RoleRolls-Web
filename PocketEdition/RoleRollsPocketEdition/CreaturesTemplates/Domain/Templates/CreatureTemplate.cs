using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Creatures.Domain
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
        public ICollection<SkillTemplate> Skills { get; set; }

        public ICollection<LifeTemplate> Lifes { get; set; }

        internal async Task AddAttributeAsync(AttributeTemplateModel attribute, RoleRollsDbContext _dbContext)
        {
            var newAttribute = new AttributeTemplate(attribute);
            Attributes.Add(newAttribute);
            await _dbContext.AttributeTemplates.AddAsync(newAttribute);
        }

        internal void RemoveAttribute(Guid attributeId, RoleRollsDbContext dbContext)
        {
            var attribute = Attributes.First(attribute => attribute.Id == attributeId);
            Attributes.Remove(attribute);
            dbContext.AttributeTemplates.Remove(attribute);
        }

        internal void UpdateAttribute(Guid attributeId, AttributeTemplateModel attributeModel, RoleRollsDbContext dbContext)
        {
            var attribute = Attributes.First(attribute => attribute.Id == attributeId);
            attribute.Update(attributeModel);
            dbContext.AttributeTemplates.Update(attribute);
        }

        internal void UpdateSkill(Guid skillId, SkillTemplateModel skillModel, RoleRollsDbContext dbContext)
        {
            var skill = Skills.First(skill => skill.Id == skillId);
            skill.Update(skillModel);
            dbContext.SkillTemplates.Update(skill);
        }

        internal void RemoveSkill(Guid skillId, RoleRollsDbContext dbContext)
        {
            var skill = Skills.First(skill => skill.Id == skillId);
            Skills.Remove(skill);
            dbContext.SkillTemplates.Remove(skill);
        }

        internal async Task AddSkill(Guid attributeId, SkillTemplateModel skill, RoleRollsDbContext dbContext)
        {
            var newSkill = new SkillTemplate(attributeId, skill);
            Skills.Add(newSkill);
            await dbContext.SkillTemplates.AddAsync(newSkill);
        }
    }
}
