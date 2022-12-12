using Microsoft.EntityFrameworkCore;
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
        private readonly ICampaignRepository _campaignRepository;

        public CampaignsService(RoleRollsDbContext dbContext, ICampaignRepository campaignRepository)
        {
            _dbContext = dbContext;
            _campaignRepository = campaignRepository;
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
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
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
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.UpdateAttribute(attributeId, attribute, _dbContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddSkill(Guid id, Guid attributeId, SkillTemplateModel skill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            await creatureTemplate.AddSkill(attributeId, skill, _dbContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveSkill(Guid id, Guid attributeId, Guid skillId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.RemoveSkill(skillId, _dbContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSkill(Guid id, Guid attributeId, Guid skillId, SkillTemplateModel skill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.UpdateSkill(skillId, skill, _dbContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddMinorSkillAsync(Guid id, Guid attributeId, Guid skillId, MinorSkillTemplateModel minorSkill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            await creatureTemplate.AddMinorSkillAsync(skillId, minorSkill, _dbContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveMinorSkillAsync(Guid id, Guid attributeId, Guid skillId, Guid minorSkillId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.RemoveMinorSkill(skillId, minorSkillId, _dbContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateMinorSkillAsync(Guid id, Guid attributeId, Guid skillId, Guid minorSkillId, MinorSkillTemplateModel minorSkill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.UpdateMinorSkill(skillId, minorSkillId, minorSkill, _dbContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddLife(Guid id, LifeTemplateModel life)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await _dbContext.CreatureTemplates
                .Include(template => template.Lifes)
                .FirstAsync(template => template.Id == campaign.CreatureTemplateId);
            await creatureTemplate.AddLifeAsync(life, _dbContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveLife(Guid id, Guid lifeId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await _dbContext.CreatureTemplates
                .Include(template => template.Lifes)
                .FirstAsync(template => template.Id == campaign.CreatureTemplateId);
            creatureTemplate.RemoveLife(lifeId, _dbContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateLife(Guid id, Guid lifeId, LifeTemplateModel life)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.UpdateLife(lifeId, life, _dbContext);
            await _dbContext.SaveChangesAsync();
        }
    }
}
