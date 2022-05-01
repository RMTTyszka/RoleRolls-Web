namespace RoleRollsPocketEdition.Creatures.Domain
{
    public class Skill : Entity
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Guid AttributeId { get; set; }
        public Guid SkillTemplateId { get; set; }
        public List<MinorSkill> MinorSkills { get; set; }
        public Skill()
        {
        }

        public Skill(SkillTemplate skill)
        {
            Id = Guid.NewGuid();
            Name = skill.Name;
            Value = skill.Value;
            AttributeId = skill.AttributeId;
            SkillTemplateId = skill.Id;
            MinorSkills = skill.MinorSkills.Select(minorSkill => new MinorSkill(minorSkill)).ToList();
        }


    }
   
}