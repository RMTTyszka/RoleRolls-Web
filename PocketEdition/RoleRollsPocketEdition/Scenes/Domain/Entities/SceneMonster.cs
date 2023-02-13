using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Scenes.Domain.Entities
{
    public class SceneMonster: Entity
    {
        public Guid SceneId { get; set; }
        public Guid MonsterId { get; set; }
        public bool Hidden { get; set; }
    }
}
