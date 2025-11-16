using RoleRollsPocketEdition.Scenes.Entities;

namespace RoleRollsPocketEdition.Campaigns.Dtos;

public abstract class SceneHistory
{
    public string Actor { get; set; }
    public DateTime AsOfDate { get; set; }
    public virtual SceneHistoryType Type { get;  }
}

public class RollSceneHistory : SceneHistory
{
    public bool Success { get; set; }
    public string Rolls { get; set; }
    public string Property { get; set; }
    public int Bonus { get; set; }
    public int Difficulty { get; set; }
    public int Complexity { get; set; }
    public override SceneHistoryType Type => SceneHistoryType.Roll;
    public Guid Id { get; set; }
}
public class ActionSceneHistory : SceneHistory
{
    public override SceneHistoryType Type => SceneHistoryType.Action;
    public string Description { get; set; }
}


