using RoleRollsPocketEdition._Domain.Creatures.Entities;

namespace RoleRollsPocketEdition._Application.Creatures.Models
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
            Attributes = creature.Attributes.Select(attribute => new AttributeModel(attribute))
                .OrderBy(a => a.Name).ToList();
            Skills = creature.Skills.Select(skill => new SkillModel(skill))
                .OrderBy(a => a.Name).ToList();
            Lifes = creature.Lifes.Select(life => new LifeModel(life))
                .OrderBy(a => a.Name).ToList();         
            Defenses = creature.Defenses.Select(defense => new DefenseModel(defense, creature))
                .OrderBy(a => a.Name).ToList();
            Name = creature.Name;
            Type = creature.Type;
        }

        public List<DefenseModel> Defenses { get; set; }

        public List<AttributeModel> Attributes { get; set; }
        public List<SkillModel> Skills { get; set; }

        public List<LifeModel> Lifes { get; set; }
        public string Name { get; set; }

        public CreatureType Type { get; set; }
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
    }
}
