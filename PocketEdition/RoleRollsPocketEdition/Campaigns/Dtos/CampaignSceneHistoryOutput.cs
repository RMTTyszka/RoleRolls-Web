using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Rolls.Entities;
using RoleRollsPocketEdition.Scenes.Entities;

namespace RoleRollsPocketEdition.Campaigns.Dtos;

public class CampaignSceneHistoryOutput
{
    public CampaignSceneHistoryOutput(Roll roll)
    {
        Id = roll.Id;
        ActorId = roll.ActorId;
        ActorType = roll.ActorType;
        Scope = SceneHistoryScope.Public;
        Text = TextFromRoll(roll);
        AsOfDate = roll.DateTime;
        Type = SceneHistoryType.Roll;
    }

    private string TextFromRoll(Roll roll)
    {
        return "";
    }

    public Guid Id { get; set; }
    public SceneHistoryType Type { get; set; }
    public Guid ActorId { get; set; }
    public ActionActorType ActorType { get; set; }
    public SceneHistoryScope Scope { get; set; }
    public DateTime AsOfDate { get; set; }
    public string Text { get; set; }
    
}