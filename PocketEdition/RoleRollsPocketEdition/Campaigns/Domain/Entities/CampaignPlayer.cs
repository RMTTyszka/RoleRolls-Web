using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Global;

namespace RoleRollsPocketEdition.Campaigns.Domain.Entities
{
    public class CampaignPlayer : Entity
    {
        public Guid CampaignId { get; set; }
        public Guid? PlayerId { get; set; }
        public Guid? InvidationCode { get; set; } 

        internal static CampaignPlayer FromInvite(Guid campaignId)
        {
            var campaignPlayer = new CampaignPlayer
            {
                CampaignId = campaignId,
                InvidationCode = Guid.NewGuid(),
            };
            return campaignPlayer;
        }
    }
}
