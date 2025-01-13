using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Scenes.Entities;

public class SceneAction : Entity
{
    public Guid ActorId { get; set; }
    public Guid SceneId { get; set; }
    public ActionActorType ActorType { get; set; }
    public string Description { get; set; }
}