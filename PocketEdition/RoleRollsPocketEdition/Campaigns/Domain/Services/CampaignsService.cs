using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Domain.Models;
using RoleRollsPocketEdition.Creatures.Domain;
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
            }



            await _dbContext.Campaigns.AddAsync(campaign);
            await _dbContext.CreatureTemplates.AddAsync(creatureTemplate);
            await _dbContext.SaveChangesAsync();
        }       
        public async Task<CampaignModel> GetAsync(Guid id) 
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var output = new CampaignModel(campaign);
            return output;
        }       
        public async Task<PagedResult<CampaignModel>> GetListAsync(PagedRequestInput input) 
        {
            var query = _dbContext.Campaigns
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .Select(campaign => new CampaignModel(campaign));
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
    }
}
