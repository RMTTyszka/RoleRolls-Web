﻿using System;
using RoleRollsPocketEdition.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Domain.Events.MinorSkills;

public class MinorSkillUpdated
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public MinorSkillTemplateModel MinorSkill { get; set; }
}