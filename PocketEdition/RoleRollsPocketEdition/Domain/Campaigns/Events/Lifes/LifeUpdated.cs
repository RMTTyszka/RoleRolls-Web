using System;
using RoleRollsPocketEdition.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Domain.Events.Lifes;

public class LifeUpdated
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public LifeTemplateModel Life { get; set; }
}