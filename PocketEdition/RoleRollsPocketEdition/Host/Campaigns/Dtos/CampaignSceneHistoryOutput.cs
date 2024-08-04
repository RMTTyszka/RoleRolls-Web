using RoleRollsPocketEdition.Domain.Campaigns.Entities;
using RoleRollsPocketEdition.Rolls.Entities;

namespace RoleRollsPocketEdition.Host.Campaigns.Dtos;

public class CampaignSceneHistoryOutput
{
    public CampaignSceneHistoryOutput(Roll roll)
    {
        Id = roll.Id;
        SourceId = roll.ActorId;
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
    public Guid SourceId { get; set; }
    public ActionActorType ActorType { get; set; }
    public SceneHistoryScope Scope { get; set; }
    public DateTime AsOfDate { get; set; }
    public string Text { get; set; }
    
}