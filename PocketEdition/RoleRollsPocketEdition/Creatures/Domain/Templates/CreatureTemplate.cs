namespace RoleRollsPocketEdition.Creatures.Domain
{
    public class CreatureTemplate : Entity
    {
        public string Name { get; set; }
        public int MaxAttributePoints => 5;
        // 5 + 4 + 3 + 2 + 2 + 1 = 17
        public int TotalAttributePoints { get; set; }
        // 4
        public int TotalSkillsPoints { get; set; }
        public virtual ICollection<AttributeTemplate> Attributes { get; set; }
        public ICollection<SkillTemplate> Skills { get; set; }

        public ICollection<LifeTemplate> Lifes { get; set; }

    }
}
