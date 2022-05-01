namespace RoleRollsPocketEdition.Creatures.Domain
{
    public class SkillTemplate : Entity
    {
        public string Name { get; set; }
        public Guid AttributeId { get; set; }

        public List<MinorSkillTemplate> MinorSkills { get; set; }

    }
}
