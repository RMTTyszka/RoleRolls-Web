using RoleRollsPocketEdition.Creatures.Domain.Entities;

namespace RoleRollsPocketEdition.Creatures.Domain.Models
{
    public class CreatureModel
    {
        public CreatureModel()
        {

        }

        public CreatureModel(Creature creature)
        {
            creature.ProcessLifes();
            Id = creature.Id;
            OwnerId = creature.OwnerId;
            Attributes = creature.Attributes.Select(attribute => new AttributeModel(attribute)).ToList();
            Skills = creature.Skills.Select(skill => new SkillModel(skill)).ToList();
            Lifes = creature.Lifes.Select(life => new LifeModel(life)).ToList();
            Name = creature.Name;
            Type = creature.Type;
        }

        public List<AttributeModel> Attributes { get; set; }
        public List<SkillModel> Skills { get; set; }

        public List<LifeModel> Lifes { get; set; }
        public string Name { get; set; }

        public CreatureType Type { get; set; }
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
    }
}
