using RoleRollsPocketEdition._Domain.Campaigns.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Powers.Entities
{
    public class PowerTemplate : Entity
    {
        public Campaign Campaign { get; set; }
        public Guid CampaignId { get; set; }
        public PowerType Type { get; set; }
        public PowerDurationType PowerDurationType { get; set; }
        public int? Duration { get; set; }
        public PowerActionType  ActionType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CastFormula { get; set; }
        public string CastDescription { get; set; }
        public Guid? UseAttributeId { get; set; }
        public Guid? TargetDefenseId { get; set; }
        public string UsagesFormula { get; set; }
        public UsageType? UsageType { get; set; }
        public ICollection<PowerInstance> Instances { get; set; }        
    }

    public enum PowerDurationType
    {
        Instant = 0,
        Turns = 1,
        Encounter = 2,
        Session = 3
    }

    public enum UsageType
    {
        Day = 0,
        Session = 1,
        Encounter = 2,
    }
}