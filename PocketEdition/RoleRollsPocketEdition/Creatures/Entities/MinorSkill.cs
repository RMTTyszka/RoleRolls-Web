﻿using RoleRollsPocketEdition.CreaturesTemplates.Entities;
using RoleRollsPocketEdition.Global;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class MinorSkill : Entity
    {
        public Guid MinorSkillTemplateId { get; set; }
        public Guid SkillId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }

        public MinorSkill()
        {
        }
        public MinorSkill(MinorSkillTemplate skill)
        {
            Id = Guid.NewGuid();
            MinorSkillTemplateId = skill.Id;
            Name = skill.Name;
        }

        public void Update(int points)
        {
            Points = points;
        }
    }
   
}