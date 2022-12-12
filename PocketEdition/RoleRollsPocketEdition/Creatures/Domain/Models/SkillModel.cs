namespace RoleRollsPocketEdition.Creatures.Domain.Models
{
    public class SkillModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public Guid AttributeId { get; set; }
        public Guid SkillTemplateId { get; set; }
        public List<MinorSkillModel> MinorSkills { get; set; }
        public SkillModel()
        {
        }

        public SkillModel(Skill skill)
        {
            Id = skill.Id;
            Name = skill.Name;
            AttributeId = skill.AttributeId;
            SkillTemplateId = skill.SkillTemplateId;
            MinorSkills = skill.MinorSkills.Select(minorSkill => new MinorSkillModel(minorSkill)).ToList();
        }
    }
}