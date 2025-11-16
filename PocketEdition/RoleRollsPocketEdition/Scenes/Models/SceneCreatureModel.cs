using RoleRollsPocketEdition.Creatures.Entities;

namespace RoleRollsPocketEdition.Scenes.Models
{
    public class SceneCreatureModel
    {
        public Guid CreatureId { get; set; }
        public CreatureCategory CreatureCategory { get; set; }
        public bool Hidden { get; set; }

    }
}


