﻿namespace RoleRollsPocketEdition.Campaigns.Events.Lifes;

public class VitalityRemoved
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public Guid VitalityId { get; set; }
}