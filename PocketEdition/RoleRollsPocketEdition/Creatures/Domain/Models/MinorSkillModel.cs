namespace RoleRollsPocketEdition.Creatures.Domain.Models
{
    public class MinorSkillModel
    {
        public Guid Id { get; set; }
        public Guid MinorSkillTemplateId { get; set; }
        public string Name { get; set; }
        public SkillProficience SkillProficience { get; set; }

        public MinorSkillModel(MinorSkill skill)
        {
            Id = skill.Id;
            MinorSkillTemplateId = skill.MinorSkillTemplateId;
            Name = skill.Name;
            SkillProficience = skill.SkillProficience;
        }
    }
}