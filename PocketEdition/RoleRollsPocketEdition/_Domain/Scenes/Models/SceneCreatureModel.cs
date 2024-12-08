using RoleRollsPocketEdition._Domain.Creatures.Entities;

namespace RoleRollsPocketEdition._Domain.Scenes.Models
{
    public class SceneCreatureModel
    {
        public Guid CreatureId { get; set; }
        public CreatureType CreatureType { get; set; }
        public bool Hidden { get; set; }

    }
}
