using System;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Global;

namespace RoleRollsPocketEdition.Scenes.Domain.Entities
{
    public class SceneCreature : Entity
    {
        public Guid SceneId { get; set; }
        public Guid CreatureId { get; set; }

        public bool Hidden { get; set; }
        public CreatureType CreatureType { get; set; }
    }
}
