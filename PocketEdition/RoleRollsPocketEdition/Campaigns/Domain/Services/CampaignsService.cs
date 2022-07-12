using RoleRollsPocketEdition.Campaigns.Domain.Models;
using RoleRollsPocketEdition.Creatures.Domain;
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
                Id = Guid.NewGuid(),
                InvitationSecret = Guid.NewGuid(),
                MasterId = campaignModel.MasterId,
                Name = campaignModel.Name,
            };

            var creatureTemplate = new CreatureTemplate
            {
                Id = Guid.NewGuid()
            };
            campaign.CreatureTemplateId = creatureTemplate.Id;

            
            await _dbContext.Campaigns.AddAsync(campaign);
            await _dbContext.CreatureTemplates.AddAsync(creatureTemplate);
        }
    }
}
