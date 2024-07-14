using System;
using RoleRollsPocketEdition.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Domain.Events.Lifes;

public class DefenseUpdated
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public DefenseTemplateModel Defenses { get; set; }
}