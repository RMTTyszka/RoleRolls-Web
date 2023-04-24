﻿using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Domain.Events;

public class LifeUpdated
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public LifeTemplateModel Life { get; set; }
}