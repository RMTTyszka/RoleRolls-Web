using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Scenes.Entities;

public class SceneInitiativeEntry : Entity
{
    public Guid SceneId { get; set; }
    public Scene Scene { get; set; } = null!;
    public Guid CreatureId { get; set; }
    public int Position { get; set; }
    public int Score { get; set; }
    public int PropertyValue { get; set; }
    public string DiceResults { get; set; } = "[]";
}
