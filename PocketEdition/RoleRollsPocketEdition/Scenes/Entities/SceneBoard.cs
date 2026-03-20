using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Scenes.Models;

namespace RoleRollsPocketEdition.Scenes.Entities;

public class SceneBoard : Entity
{
    public Guid SceneId { get; set; }
    public Scene Scene { get; set; } = null!;
    public int Version { get; set; }
    public SceneBoardState State { get; set; } = SceneBoardState.CreateEmpty();
    public DateTimeOffset UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
}
