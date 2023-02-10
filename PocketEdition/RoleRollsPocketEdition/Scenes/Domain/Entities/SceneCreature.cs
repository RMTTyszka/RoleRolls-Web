using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Scenes.Domain.Entities
{
    public class SceneCreature : Entity
    {
        public Guid SceneId { get; set; }
        public Guid CreatureId { get; set; }
    }
}
