using System;

namespace RoleRollsPocketEdition.Campaigns.Domain.Events;

public class CampaignUpdated
{
    public Guid CampaingId { get; set; }
}