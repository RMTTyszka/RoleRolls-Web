using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition.Domain.Campaigns.Entities;

public class SceneAction : Entity
{
    public Guid ActorId { get; set; }
    public Guid SceneId { get; set; }
    public ActionActorType ActorType { get; set; }
    public string Description { get; set; }
}