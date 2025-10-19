using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Powers.Entities
{
    public class PowerTemplate : Entity, IHaveBonuses
    {
        public CampaignTemplate CampaignTemplate { get; set; }
        public Guid CampaignTemplateId { get; set; }
        public PowerType Type { get; set; }
        public PowerDurationType PowerDurationType { get; set; }
        public int? Duration { get; set; }
        public PowerActionType ActionType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CastFormula { get; set; }
        public string CastDescription { get; set; }
        public Guid? UseAttributeId { get; set; }
        public Guid? TargetDefenseId { get; set; }
        public string UsagesFormula { get; set; }
        public PowerUsageType? UsageType { get; set; }
        public TargetType TargetType { get; set; }
        public List<Bonus> Bonuses { get; set; }
        public ICollection<PowerInstance> Instances { get; set; }
        public ICollection<ItemTemplate> ItemTemplates { get; set; }
    }

    public enum TargetType
    {
        Self = 0,
        Target = 1,
        Both = 2
    }

    public enum PowerDurationType
    {
        Instant = 0,
        Turns = 1,
        Encounter = 2,
        Session = 3,
        Continuous = 4,
    }

    public enum PowerUsageType
    {
        Day = 0,
        Session = 1,
        Encounter = 2,
        Continuous = 3,
        Resource = 4
    }
}