using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;

namespace RoleRollsPocketEdition.Scenes.Entities
{
    public class SceneCreature : Entity
    {
        public Guid SceneId { get; set; }
        public Scene Scene { get; set; }
        public Guid CreatureId { get; set; }

        public bool Hidden { get; set; }
        public CreatureCategory CreatureCategory { get; set; }
    }
}
