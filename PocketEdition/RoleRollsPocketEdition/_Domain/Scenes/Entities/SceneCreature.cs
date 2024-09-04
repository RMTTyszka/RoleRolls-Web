using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Domain.Creatures.Entities;

namespace RoleRollsPocketEdition.Domain.Scenes.Entities
{
    public class SceneCreature : Entity
    {
        public Guid SceneId { get; set; }
        public Guid CreatureId { get; set; }

        public bool Hidden { get; set; }
        public CreatureType CreatureType { get; set; }
    }
}
