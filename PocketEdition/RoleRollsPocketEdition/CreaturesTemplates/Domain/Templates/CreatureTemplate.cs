using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;

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
            Skills = template.Skills.Select(skill => new SkillTemplate(skill)).ToList();
            Lifes = template.Lifes.Select(life => new LifeTemplate(life)).ToList();
        }

        public string Name { get; set; }
        public int MaxAttributePoints => 5;
        // 5 + 4 + 3 + 2 + 2 + 1 = 17
        public int TotalAttributePoints { get; set; }
        // 4
        public int TotalSkillsPoints { get; set; }
        public virtual ICollection<AttributeTemplate> Attributes { get; set; }
        public ICollection<SkillTemplate> Skills { get; set; }

        public ICollection<LifeTemplate> Lifes { get; set; }

        internal void AddAttribute(AttributeTemplateModel attribute)
        {
            Attributes.Add(new AttributeTemplate(attribute));
        }
    }
}
