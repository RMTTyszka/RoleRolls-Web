namespace RoleRollsPocketEdition.Creatures.Domain
{
    public class MinorSkill : Entity
    {
        public Guid MinorSkillTemplateId { get; set; }
        public string Name { get; set; }
        public SkillProficience SkillProficience { get; set; }

        public MinorSkill()
        {
        }
        public MinorSkill(MinorSkillTemplate skill)
        {
            Id = Guid.NewGuid();
            MinorSkillTemplateId = skill.Id;
            Name = skill.Name;
            SkillProficience = skill.SkillProficience;
        }
    }
   
}