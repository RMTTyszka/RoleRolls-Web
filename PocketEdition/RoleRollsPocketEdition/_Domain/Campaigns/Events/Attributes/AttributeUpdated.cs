﻿using RoleRollsPocketEdition.Application.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.Domain.Campaigns.Events.Attributes;

public class AttributeUpdated
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public AttributeTemplateModel Attribute { get; set; }
}