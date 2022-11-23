﻿using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos
{
    public class CreatureTemplateModel
    {

        public CreatureTemplateModel()
        {

        }
        public CreatureTemplateModel(CreatureTemplate template)
        {
            Id = template.Id;
            Name = template.Name;
            TotalAttributePoints = template.TotalAttributePoints;
            MaxAttributePoints = template.MaxAttributePoints;
            TotalSkillsPoints = template.TotalSkillsPoints;
            Attributes = template.Attributes.Select(attribute => new AttributeTemplateModel(attribute)).ToList();
            Skills = template.Skills.Select(skill => new SkillTemplateModel(skill)).ToList();
            Lifes = template.Lifes.Select(life => new LifeTemplateModel(life)).ToList();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaxAttributePoints { get; init; }
        public int TotalAttributePoints { get; set; }
        // 4
        public int TotalSkillsPoints { get; set; }
        public virtual ICollection<AttributeTemplateModel> Attributes { get; set; }
        public ICollection<SkillTemplateModel> Skills { get; set; }

        public ICollection<LifeTemplateModel> Lifes { get; set; }

        public Dictionary<Guid, List<SkillTemplateModel>> SkillsByAttribute => Skills
            .GroupBy(skill => skill.AttributeId)
            .ToDictionary(groupedSkill => groupedSkill.Key, groupedSkill => groupedSkill
            .ToList());
    }
}
