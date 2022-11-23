﻿using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Domain.Models;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;
using RoleRollsPocketEdition.Global.Dtos;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Campaigns.Domain.Services
{
    public class CampaignsService : ICampaignsService
    {
        private readonly RoleRollsDbContext _dbContext;

        public CampaignsService(RoleRollsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private async Task<CreatureTemplate> GetCreatureTemplateAggregateAsync(Guid id) 
        {
            var creatureTemplate = await _dbContext.CreatureTemplates
            .Include(template => template.Attributes)
            .Include(template => template.Skills)
            .FirstAsync(template => template.Id == id);
            return creatureTemplate;
        }

        public async Task CreateAsync(CampaignModel campaignModel) 
        {
            var campaign = new Campaign
            {
                Id = campaignModel.Id,
                InvitationSecret = Guid.NewGuid(),
                MasterId = campaignModel.MasterId,
                Name = campaignModel.Name,
            };

            var creatureTemplate = new CreatureTemplate();
            if (!campaignModel.CreatureTemplateId.HasValue)
            {
                creatureTemplate = new CreatureTemplate
                {
                    Id = Guid.NewGuid(),
                };
                creatureTemplate.Name = campaign.Name;
                campaign.CreatureTemplateId = creatureTemplate.Id;
            }
            else 
            {
                campaign.CreatureTemplateId = campaignModel.CreatureTemplateId.Value;
                creatureTemplate = await _dbContext.CreatureTemplates.FindAsync(campaign.CreatureTemplateId);
            }

            await _dbContext.Campaigns.AddAsync(campaign);
            if (!campaignModel.CreatureTemplateId.HasValue) 
            { 
                await _dbContext.CreatureTemplates.AddAsync(creatureTemplate);
            }
            await _dbContext.SaveChangesAsync();
        }       
        public async Task<CampaignModel> GetAsync(Guid id) 
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await _dbContext.CreatureTemplates
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .FirstAsync(template => template.Id == campaign.CreatureTemplateId);
            var output = new CampaignModel(campaign, creatureTemplate);
            return output;
        }       
        public async Task<PagedResult<CampaignModel>> GetListAsync(PagedRequestInput input) 
        {
            var query = _dbContext.Campaigns
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .Select(campaign => new CampaignModel(campaign, null));
            var campaigns = await query.ToListAsync();
            var totalCount = await query.CountAsync();
            var output = new PagedResult<CampaignModel>
            { 
                Content = campaigns,
                TotalElements = totalCount
            };
            return output;
        }
               
        public async Task DeleteAsync(Guid id) 
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            if (campaign is not null) 
            {
                _dbContext.Campaigns.Remove(campaign);
                 await _dbContext.SaveChangesAsync();
            }
        }                  
        public async Task UpdateAsync(CampaignModel campaignModel) 
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignModel.Id);
            campaign.Name = campaignModel.Name;

            _dbContext.Campaigns.Update(campaign);
            await _dbContext.SaveChangesAsync();
        }      
        public async Task AddAttribute(Guid campaignId, AttributeTemplateModel attribute) 
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _dbContext.CreatureTemplates
                .Include(template => template.Attributes)
                .FirstAsync(template => template.Id == campaign.CreatureTemplateId);
            await creatureTemplate.AddAttributeAsync(attribute, _dbContext);
            await _dbContext.SaveChangesAsync();
        }         
        public async Task RemoveAttribute(Guid campaignId, Guid attributeId) 
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _dbContext.CreatureTemplates
                .Include(template => template.Attributes)
                .FirstAsync(template => template.Id == campaign.CreatureTemplateId);
            creatureTemplate.RemoveAttribute(attributeId, _dbContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAttribute(Guid id, Guid attributeId, AttributeTemplateModel attribute)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.UpdateAttribute(attributeId, attribute, _dbContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddSkill(Guid id, Guid attributeId, SkillTemplateModel skill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            await creatureTemplate.AddSkill(attributeId, skill, _dbContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveSkill(Guid id, Guid attributeId, Guid skillId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.RemoveSkill(skillId, _dbContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSkill(Guid id, Guid attributeId, Guid skillId, SkillTemplateModel skill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.UpdateSkill(skillId, skill, _dbContext);
            await _dbContext.SaveChangesAsync();
        }
    }
}
