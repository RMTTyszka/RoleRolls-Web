namespace RoleRollsPocketEdition.Creatures.Domain.Models
{
    public class CreatureModel
    {

        public CreatureModel(Creature creature)
        {
            this.Attributes = creature.Attributes.Select(attribute => new AttributeModel(attribute)).ToList();
            this.Skills = creature.Skills.Select(skill => new SkillModel(skill)).ToList();
            this.Lifes = creature.Lifes.Select(life => new LifeModel(life)).ToList();
        }

        public List<AttributeModel> Attributes { get; set; }
        public List<SkillModel> Skills { get; set; }

        public List<LifeModel> Lifes { get; set; }
        public string Name { get; set; }

    }
}
