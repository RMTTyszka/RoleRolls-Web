﻿using RoleRollsPocketEdition._Application.Creatures;
using RoleRollsPocketEdition._Application.Creatures.Models;
using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Creatures.Entities
{
    public class Skill : Entity
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Guid AttributeId { get; set; }
        public Guid SkillTemplateId { get; set; }
        public List<MinorSkill> MinorSkills { get; set; }

        public int PointsLimit => Math.Max(3 + MinorSkills.Count - 1, 0); 
        public int UsedPoints => MinorSkills.Sum(minorSkill => minorSkill.Points); 
        public Skill()
        {
        }

        public Skill(SkillTemplate skill, Attribute attribute)
        {
            Id = Guid.NewGuid();
            Name = skill.Name;
            AttributeId = attribute.Id;
            SkillTemplateId = skill.Id;
            MinorSkills = skill.MinorSkills.Select(minorSkill => new MinorSkill(minorSkill)).ToList();
        }

        public CreatureUpdateValidationResult Update(SkillModel updatedSkill)
        {
            Value = updatedSkill.Value;
            foreach (var minorSkill in MinorSkills)
            {
                var updatedMinorSkill = updatedSkill.MinorSkills.First(minorsk => minorsk.MinorSkillTemplateId == minorSkill.MinorSkillTemplateId);
                var totalPointAfterUpdate = UsedPoints - minorSkill.Points + updatedMinorSkill.Points;
                if (totalPointAfterUpdate > PointsLimit)
                {
                    return new CreatureUpdateValidationResult(CreatureUpdateValidation.SkillPointsGreaterThanAllowed,
                        Name);
                }

                minorSkill.Update(updatedMinorSkill.Points);
            }
            return new CreatureUpdateValidationResult(CreatureUpdateValidation.Ok, null);
        }
    }
   
}