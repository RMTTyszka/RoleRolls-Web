using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Dtos;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Campaigns.Models;
using RoleRollsPocketEdition.Core.Authentication.Application.Services;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Core.EntityFramework;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Templates.Dtos;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Campaigns.ApplicationServices
{
    public class CampaignsService : ICampaignsService
    {
        private readonly RoleRollsDbContext _dbContext;
        private readonly ICampaignRepository _campaignRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public CampaignsService(RoleRollsDbContext dbContext, ICampaignRepository campaignRepository, ICurrentUser currentUser, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _campaignRepository = campaignRepository;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }


        public async Task CreateAsync(CampaignCreateInput campaignModel)
        {
            campaignModel.MasterId ??= _currentUser.User.Id;
            var campaign = new Campaign(campaignModel);

            if (campaignModel.CampaignTemplateId.HasValue)
            {
                var template = await _dbContext.CampaignTemplates.FirstAsync(x => x.Id == campaignModel.CampaignTemplateId);
                campaign.CampaignTemplate = template;
            }
            else
            {
                var template = new CampaignTemplate(campaignModel);
                campaign.CampaignTemplate = template;
            }

                await _dbContext.Campaigns.AddAsync(campaign);
                await _dbContext.SaveChangesAsync();
        }       
        public async Task<CampaignModel> GetAsync(Guid id) 
        {
            var campaign = await _dbContext.Campaigns
                .FirstAsync(e => e.Id == id);

            var template = await _dbContext.Campaigns
                .Include(c => c.CampaignTemplate)
                .Where(e => e.Id == id)
                .Select(e => e.CampaignTemplate)
                .FirstAsync();
            campaign.CampaignTemplate = template;
            var attributes = await _dbContext.Campaigns
                .Include(c => c.CampaignTemplate)
                .ThenInclude(a => a.Attributes)
                .Where(e => e.Id == id)
                .Select(e => e.CampaignTemplate.Attributes)
                .FirstAsync();

            campaign.CampaignTemplate.Attributes = attributes;

            var attributelessSkills = await _dbContext.Campaigns
                .Include(c => c.CampaignTemplate)
                .ThenInclude(a => a.AttributelessSkills)
                .Where(e => e.Id == id)
                .Select(e => e.CampaignTemplate.AttributelessSkills)
                .FirstAsync();

            campaign.CampaignTemplate.AttributelessSkills = attributelessSkills;

            var defenses = await _dbContext.Campaigns
                .Include(c => c.CampaignTemplate)
                .ThenInclude(t => t.Defenses)
                .Where(e => e.Id == id)
                .Select(e => e.CampaignTemplate.Defenses)
                .FirstAsync();

            campaign.CampaignTemplate.Defenses = defenses;

            var vitalities = await _dbContext.Campaigns
                .Include(c => c.CampaignTemplate)
                .ThenInclude(t => t.Vitalities)
                .Where(e => e.Id == id)
                .Select(e => e.CampaignTemplate.Vitalities)
                .FirstAsync();

            campaign.CampaignTemplate.Vitalities = vitalities;
            
            var creatureTypes = await _dbContext.Campaigns
                .Include(c => c.CampaignTemplate)
                .ThenInclude(t => t.CreatureTypes)
                .Where(e => e.Id == id)
                .Select(e => e.CampaignTemplate.CreatureTypes)
                .FirstAsync();

            campaign.CampaignTemplate.CreatureTypes = creatureTypes;            
            
            var damageTypes = await _dbContext.Campaigns
                .Include(c => c.CampaignTemplate)
                .ThenInclude(t => t.DamageTypes)
                .Where(e => e.Id == id)
                .Select(e => e.CampaignTemplate.DamageTypes)
                .FirstAsync();

            campaign.CampaignTemplate.DamageTypes = damageTypes;           
            
            var archetypes = await _dbContext.Campaigns
                .Include(c => c.CampaignTemplate)
                .ThenInclude(t => t.Archetypes)
                .Where(e => e.Id == id)
                .Select(e => e.CampaignTemplate.Archetypes)
                .FirstAsync();

            campaign.CampaignTemplate.Archetypes = archetypes;

            var itemConfiguration = await _dbContext.Campaigns
                .Include(c => c.CampaignTemplate)
                .ThenInclude(c => c.ItemConfiguration)
                .Where(e => e.Id == id)
                .Select(e => e.CampaignTemplate.ItemConfiguration)
                .FirstAsync();

            campaign.CampaignTemplate.ItemConfiguration = itemConfiguration;

            var output = new CampaignModel(campaign);
            return output;
        }       
        public async Task<PagedResult<CampainView>> GetListAsync(PagedRequestInput input)
        {
            var query = (from campaign in _dbContext.Campaigns
                        .Include(c => c.CampaignTemplate)
                        .AsNoTracking()
                join invited in _dbContext.CampaignPlayers
                        .Where(player => player.PlayerId == _currentUser.User.Id)
                    on campaign.Id equals invited.CampaignId into groupedPlayers
                where campaign.MasterId == _currentUser.User.Id || groupedPlayers.Any()
                select campaign)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .Select(campaign => new CampainView(campaign));
            var campaigns = await query.ToListAsync();
            var totalCount = await query.CountAsync();
            var output = new PagedResult<CampainView>
            { 
                Items = campaigns,
                TotalCount = totalCount
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
        public async Task UpdateAsync(Guid id, CampaignUpdateInput campaignModel) 
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            campaign.Name = campaignModel.Name;
            _dbContext.Campaigns.Update(campaign);
            await _dbContext.SaveChangesAsync();
        }      
        public async Task AddAttribute(Guid campaignId, AttributeTemplateModel attribute) 
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _dbContext.CampaignTemplates
                .Include(template => template.Attributes)
                .FirstAsync(template => template.Id == campaign.CampaignTemplateId);
            await creatureTemplate.AddAttributeAsync(attribute, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
        }         
        public async Task RemoveAttribute(Guid campaignId, Guid attributeId) 
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _dbContext.CampaignTemplates
                .Include(template => template.Attributes)
                .FirstAsync(template => template.Id == campaign.CampaignTemplateId);
            creatureTemplate.RemoveAttribute(attributeId, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateAttribute(Guid id, Guid attributeId, AttributeTemplateModel attribute)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);
            creatureTemplate.UpdateAttribute(attributeId, attribute, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();

        }

        public async Task AddSkill(Guid campaignId, Guid? attributeId, SkillTemplateModel skill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);
            await creatureTemplate.AddSkill(attributeId, skill, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();

        }

        public async Task RemoveSkill(Guid campaignId, Guid? attributeId, Guid skillId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);
            creatureTemplate.RemoveSkill(skillId, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
 
        }

        public async Task UpdateSkill(Guid campaignId, Guid? attributeId, Guid skillId, SkillTemplateModel skill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);
            creatureTemplate.UpdateSkill(skillId, skill, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();

        }

        public async Task AddMinorSkillAsync(Guid campaignId, Guid? attributeId, Guid skillId, SpecificSkillTemplateModel specificSkill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);
            await creatureTemplate.AddMinorSkillAsync(skillId, specificSkill, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();

        }

        public async Task RemoveMinorSkillAsync(Guid campaignId, Guid? attributeId, Guid skillId, Guid minorSkillId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);
            creatureTemplate.RemoveMinorSkill(skillId, minorSkillId, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);

            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateMinorSkillAsync(Guid campaignId, Guid? attributeId, Guid skillId, Guid minorSkillId, SpecificSkillTemplateModel specificSkill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);
            creatureTemplate.UpdateMinorSkill(skillId, minorSkillId, specificSkill, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);

            await _dbContext.SaveChangesAsync();

        }

        public async Task AddVitality(Guid campaignId, VitalityTemplateModel vitality)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _dbContext.CampaignTemplates
                .Include(template => template.Vitalities)
                .FirstAsync(template => template.Id == campaign.CampaignTemplateId);
            await creatureTemplate.AddVitalityAsync(vitality, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();

        }

        public async Task RemoveVitality(Guid campaignId, Guid vitalityId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _dbContext.CampaignTemplates
                .Include(template => template.Vitalities)
                .FirstAsync(template => template.Id == campaign.CampaignTemplateId);
            creatureTemplate.RemoveVitality(vitalityId, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateVitality(Guid campaignId, Guid vitalityId, VitalityTemplateModel vitality)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);
            creatureTemplate.UpdateVitality(vitalityId, vitality, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<CampaignPlayerModel>> GetPlayersAsync(Guid campaignId)
        {
            var players = await _dbContext.CampaignPlayers.Where(player => player.CampaignId == campaignId && player.PlayerId.HasValue)
                .AsNoTracking()
                .Select(player => new CampaignPlayerModel(player))
                .ToListAsync();
            return players;
        }

        public async Task<Guid> Invite(Guid campaignId)
        {
            var campaignPlayer = CampaignPlayer.FromInvite(campaignId);
            await _dbContext.CampaignPlayers.AddAsync(campaignPlayer);
            await _dbContext.SaveChangesAsync();
            return campaignPlayer.InvidationCode.Value;
;       }

        public async Task AddDefense(Guid campaignId, DefenseTemplateModel defense)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _dbContext.CampaignTemplates
                .Include(template => template.Vitalities)
                .FirstAsync(template => template.Id == campaign.CampaignTemplateId);
            var defenseAdded = await creatureTemplate.AddDefenseAsync(defense, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveDefense(Guid campaignId, Guid defenseId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _dbContext.CampaignTemplates
                .Include(template => template.Defenses)
                .FirstAsync(template => template.Id == campaign.CampaignTemplateId);
            var defenseRemoved = creatureTemplate.RemoveDefense(defenseId, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateDefense(Guid campaignId, Guid defenseId, DefenseTemplateModel defense)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);
            var defenseUpdate = creatureTemplate.UpdateDefense(defense, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ValidationResult<InvitationResult>> AcceptInvite(Guid playerId, Guid invitationCode)
        {
            var invitationResult = new ValidationResult<InvitationResult>();
            var campaignPlayer = await _dbContext.CampaignPlayers.FirstOrDefaultAsync(campaignPlayer => campaignPlayer.InvidationCode == invitationCode);
            if (campaignPlayer is not null)
            {
                campaignPlayer.PlayerId = playerId;
                _dbContext.CampaignPlayers.Update(campaignPlayer);
                await _dbContext.SaveChangesAsync();
                invitationResult.Result = InvitationResult.Ok;
                return invitationResult;
            }
            invitationResult.Result = InvitationResult.WrongCode;
            return invitationResult;
;       }
    }
}
