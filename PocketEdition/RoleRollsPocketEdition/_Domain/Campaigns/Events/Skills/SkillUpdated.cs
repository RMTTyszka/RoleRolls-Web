﻿using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition._Domain.Campaigns.Events.Skills;

public class SkillUpdated
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public SkillTemplateModel Skill { get; set; }
}