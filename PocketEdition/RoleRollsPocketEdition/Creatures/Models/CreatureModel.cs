using RoleRollsPocketEdition.Archetypes.Models;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.CreatureTypes.Models;

namespace RoleRollsPocketEdition.Creatures.Models
{
    public class CreatureModel
    {
        public CreatureModel()
        {

        }

        public CreatureModel(Creature creature)
        {
            Id = creature.Id;
            OwnerId = creature.OwnerId;
            Attributes = creature.Attributes.Select(attribute => new AttributeModel(attribute))
                .OrderBy(a => a.Name).ToList();
            Skills = creature.Skills.Select(skill => new SkillModel(skill))
                .OrderBy(a => a.Name).ToList();
            Vitalities = creature.Vitalities.Select(vitality => new VitalityModel(vitality))
                .OrderBy(a => a.Name).ToList();         
            Defenses = creature.Defenses.Select(defense => new DefenseModel(defense, creature))
                .OrderBy(a => a.Name).ToList();
            Name = creature.Name;
            Level = creature.Level;
            Category = creature.Category;
            Inventory = InventoryModel.FromCreature(creature);
            Equipment = EquipmentModel.FromCreature(creature);
        }


        public EquipmentModel Equipment { get; set; }

        public List<DefenseModel> Defenses { get; set; }

        public List<AttributeModel> Attributes { get; set; }
        public List<SkillModel> Skills { get; set; }

        public List<VitalityModel> Vitalities { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

        public CreatureCategory Category { get; set; }
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public InventoryModel Inventory { get; set; }
        public ArchetypeModel? Archetype { get; set; }
        public CreatureTypeModel? CreatureType { get; set; }
        public bool IsTemplate { get; set; }
    }
}
