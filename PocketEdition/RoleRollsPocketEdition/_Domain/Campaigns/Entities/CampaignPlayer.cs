﻿using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Campaigns.Entities
{
    public class CampaignPlayer : Entity
    {
        public Guid CampaignId { get; set; }
        public Guid? PlayerId { get; set; }
        public Guid? InvidationCode { get; set; } 

        public static CampaignPlayer FromInvite(Guid campaignId)
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
