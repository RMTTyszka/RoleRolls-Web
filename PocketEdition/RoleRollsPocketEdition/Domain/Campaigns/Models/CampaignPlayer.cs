using System;
using RoleRollsPocketEdition.Domain.Campaigns.Entities;

namespace RoleRollsPocketEdition.Campaigns.Domain.Models
{
    public class CampaignPlayerModel
    {
        public CampaignPlayerModel(CampaignPlayer player)
        {
            Id = player.Id;
            CampaignId = player.CampaignId;
            PlayerId = player.PlayerId;
        }

        public Guid Id { get; set; }
        public Guid CampaignId { get; set; }
        public Guid? PlayerId { get; set; }
        public Guid? InvidationCode { get; set; }
    }
}
