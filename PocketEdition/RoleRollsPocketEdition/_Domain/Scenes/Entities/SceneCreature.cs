using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Scenes.Entities
{
    public class SceneCreature : Entity
    {
        public Guid SceneId { get; set; }
        public Guid CreatureId { get; set; }

        public bool Hidden { get; set; }
        public CreatureType CreatureType { get; set; }
    }
}
