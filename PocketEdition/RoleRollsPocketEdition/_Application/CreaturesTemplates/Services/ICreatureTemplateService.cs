﻿using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition._Application.CreaturesTemplates.Services
{
    public interface ICreatureTemplateService
    {
        public Task<CampaignTemplateModel> Get(Guid id);
        public Task Create(CampaignTemplateModel template);
        public Task<CreatureTemplateValidationResult> UpdateAsync(Guid id, CampaignTemplateModel template);
    }
}
