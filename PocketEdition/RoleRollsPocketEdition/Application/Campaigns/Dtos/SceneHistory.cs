using Newtonsoft.Json;
using RoleRollsPocketEdition.Domain.Campaigns.Entities;

namespace RoleRollsPocketEdition.Application.Campaigns.Dtos;

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

}

