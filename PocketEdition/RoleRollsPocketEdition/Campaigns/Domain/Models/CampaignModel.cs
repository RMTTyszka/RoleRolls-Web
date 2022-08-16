namespace RoleRollsPocketEdition.Campaigns.Domain.Models
{
    public class CampaignModel
    {
        public Guid Id { get; set; }
        public Guid MasterId { get; set; }
        public Guid? CreatureTemplateId{ get; set; }
        public string Name { get; set; }

        public CampaignModel()
        {
                
        }
        public CampaignModel(Campaign campaign)
        {
            Id = campaign.Id;
            MasterId = campaign.MasterId;
            Name = campaign.Name;
            CreatureTemplateId = campaign.CreatureTemplateId;
        }
    }
}
