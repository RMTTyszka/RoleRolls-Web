using RoleRollsPocketEdition._Domain.Creatures.Entities;

namespace RoleRollsPocketEdition._Application.Creatures.Models
{
    public class DefenseModel
    {

        public DefenseModel()
        {

        }
        public Guid Id { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public Guid DefenseTemplateId { get; set; }

        public DefenseModel(Defense defense, Creature creature)
        {
            Id = defense.Id;
            DefenseTemplateId = defense.DefenseTemplateId;
            Value = creature.ApplyFormula(defense.Formula);
            Name = defense.Name;
        }
    }
}