﻿using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition.Domain.Scenes.Entities
{
    public class SceneMonster: Entity
    {
        public Guid SceneId { get; set; }
        public Guid MonsterId { get; set; }
        public bool Hidden { get; set; }
    }
}