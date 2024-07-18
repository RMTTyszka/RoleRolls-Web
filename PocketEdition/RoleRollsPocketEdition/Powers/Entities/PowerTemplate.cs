using RoleRollsPocketEdition.Campaigns.Domain.Entities;
using RoleRollsPocketEdition.Global;

namespace RoleRollsPocketEdition.Powers.Entities
{
    public class PowerTemplate : Entity
    {
        public Campaign Campaign { get; set; }
        public Guid CampaignId { get; set; }
        public PowerType Type { get; set; }
        public PowerActionType  ActionType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? UseAttributeId { get; set; }
        public Guid? TargetDefenseId { get; set; }
        public int? Useges { get; set; }
        public UsageType? UsageType { get; set; }
    }

    public enum UsageType
    {
        Day = 0,
        Session = 1,
        Encounter = 2,
    }
}