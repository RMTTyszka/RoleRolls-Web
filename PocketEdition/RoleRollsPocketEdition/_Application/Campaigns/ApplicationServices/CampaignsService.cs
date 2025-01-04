using MassTransit;
using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Application.Campaigns.Dtos;
using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition._Domain.Campaigns;
using RoleRollsPocketEdition._Domain.Campaigns.Entities;
using RoleRollsPocketEdition._Domain.Campaigns.Events.Attributes;
using RoleRollsPocketEdition._Domain.Campaigns.Events.Lifes;
using RoleRollsPocketEdition._Domain.Campaigns.Events.MinorSkills;
using RoleRollsPocketEdition._Domain.Campaigns.Events.Skills;
using RoleRollsPocketEdition._Domain.Campaigns.Models;
using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;
using RoleRollsPocketEdition.Authentication.Application.Services;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.Campaigns.ApplicationServices
{
    public class CampaignsService : ICampaignsService
    {
        private readonly RoleRollsDbContext _dbContext;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IBus _bus;
        private readonly ICurrentUser _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public CampaignsService(RoleRollsDbContext dbContext, ICampaignRepository campaignRepository, IBus bus, ICurrentUser currentUser, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _campaignRepository = campaignRepository;
            _bus = bus;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }


        public async Task CreateAsync(CampaignModel campaignModel)
        {
            var campaign = Campaign.InstantiateNewCampaign(campaignModel);

            var creatureTemplate = new CampaignTemplate();
            if (!campaignModel.CampaignTemplateId.HasValue)
            {
                creatureTemplate = new CampaignTemplate
                {
                    Id = campaign.Id
                };
                creatureTemplate.Name = campaign.Name;
                campaign.CreatureTemplateId = creatureTemplate.Id;
                campaign.CampaignTemplate = creatureTemplate;
            }
            else
            {
                var template = await _dbContext.CampaignTemplates.FirstAsync(x => x.Id == campaignModel.CampaignTemplateId);
                campaign.CampaignTemplate = template;
            }

            using (_unitOfWork.Begin())
            {
                if (!campaignModel.CampaignTemplateId.HasValue) 
                { 
                    await _dbContext.CampaignTemplates.AddAsync(creatureTemplate);
                }
                await _dbContext.Campaigns.AddAsync(campaign);
                _unitOfWork.Commit();
            }
        }       
        public async Task<CampaignModel> GetAsync(Guid id) 
        {
            var campaign = await _dbContext.Campaigns
                .Include(c => c.CampaignTemplate)
                .ThenInclude(t => t.Attributes)
                .ThenInclude(a => a.SkillTemplates)
                .ThenInclude(s => s.MinorSkills)
                .Include(c => c.CampaignTemplate)
                .ThenInclude(t => t.Defenses)          
                .Include(c => c.CampaignTemplate)
                .ThenInclude(t => t.Lifes)      
                .Include(c => c.CampaignTemplate)
                .Include(c => c.ItemConfiguration)
                .FirstAsync(e => e.Id == id);
            var output = new CampaignModel(campaign);
            return output;
        }       
        public async Task<PagedResult<CampaignModel>> GetListAsync(PagedRequestInput input)
        {
            var query = (from campaign in _dbContext.Campaigns
                        .AsNoTracking()
                join invited in _dbContext.CampaignPlayers
                        .Where(player => player.PlayerId == _currentUser.User.Id)
                    on campaign.Id equals invited.CampaignId into groupedPlayers
                where campaign.MasterId == _currentUser.User.Id || groupedPlayers.Any()
                select campaign)
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
            var creatureTemplate = await _dbContext.CampaignTemplates
                .Include(template => template.Attributes)
                .FirstAsync(template => template.Id == campaign.CreatureTemplateId);
            await creatureTemplate.AddAttributeAsync(attribute, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(new AttributeAdded
            {
                Attribute = attribute,
                CampaignId = campaignId,
                CreatureTemplateId = creatureTemplate.Id
            });
        }         
        public async Task RemoveAttribute(Guid campaignId, Guid attributeId) 
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _dbContext.CampaignTemplates
                .Include(template => template.Attributes)
                .FirstAsync(template => template.Id == campaign.CreatureTemplateId);
            creatureTemplate.RemoveAttribute(attributeId, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(new AttributeRemoved
            {
                AttributeId = attributeId,
                CampaignId = campaignId,
                CreatureTemplateId = creatureTemplate.Id
            });
        }

        public async Task UpdateAttribute(Guid id, Guid attributeId, AttributeTemplateModel attribute)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.UpdateAttribute(attributeId, attribute, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(new AttributeUpdated
            {
                Attribute = attribute,
                CampaignId = id,
                CreatureTemplateId = creatureTemplate.Id
            });
        }

        public async Task AddSkill(Guid campaignId, Guid attributeId, SkillTemplateModel skill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            await creatureTemplate.AddSkill(attributeId, skill, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(new SkillAdded()
            {
                Skill = skill,
                CampaignId = campaignId,
                CreatureTemplateId = creatureTemplate.Id
            });
        }

        public async Task RemoveSkill(Guid campaignId, Guid attributeId, Guid skillId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.RemoveSkill(skillId, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(new SkillRemoved()
            {
                SkillId = skillId,
                CampaignId = campaignId,
                CreatureTemplateId = creatureTemplate.Id
            });  
        }

        public async Task UpdateSkill(Guid campaignId, Guid attributeId, Guid skillId, SkillTemplateModel skill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.UpdateSkill(skillId, skill, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(new SkillUpdated()
            {
                Skill = skill,
                CampaignId = campaignId,
                CreatureTemplateId = creatureTemplate.Id
            });
        }

        public async Task AddMinorSkillAsync(Guid campaignId, Guid attributeId, Guid skillId, MinorSkillTemplateModel minorSkill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            await creatureTemplate.AddMinorSkillAsync(skillId, minorSkill, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(new MinorSkillAdded
            {
                MinorSkill = minorSkill,
                CampaignId = campaignId,
                CreatureTemplateId = creatureTemplate.Id
            });
        }

        public async Task RemoveMinorSkillAsync(Guid campaignId, Guid attributeId, Guid skillId, Guid minorSkillId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.RemoveMinorSkill(skillId, minorSkillId, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);

            await _dbContext.SaveChangesAsync();
            await _bus.Publish(new MinorSkillRemoved
            {
                MinorSkillId = minorSkillId,
                CampaignId = campaignId,
                CreatureTemplateId = creatureTemplate.Id
            });
        }

        public async Task UpdateMinorSkillAsync(Guid campaignId, Guid attributeId, Guid skillId, Guid minorSkillId, MinorSkillTemplateModel minorSkill)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.UpdateMinorSkill(skillId, minorSkillId, minorSkill, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);

            await _dbContext.SaveChangesAsync();
            await _bus.Publish(new MinorSkillUpdated
            {
                MinorSkill = minorSkill,
                CampaignId = campaignId,
                CreatureTemplateId = creatureTemplate.Id
            });
        }

        public async Task AddLife(Guid campaignId, LifeTemplateModel life)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _dbContext.CampaignTemplates
                .Include(template => template.Lifes)
                .FirstAsync(template => template.Id == campaign.CreatureTemplateId);
            await creatureTemplate.AddLifeAsync(life, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(new LifeAdded
            {
                Life = life,
                CampaignId = campaignId,
                CreatureTemplateId = creatureTemplate.Id
            });
        }

        public async Task RemoveLife(Guid campaignId, Guid lifeId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _dbContext.CampaignTemplates
                .Include(template => template.Lifes)
                .FirstAsync(template => template.Id == campaign.CreatureTemplateId);
            creatureTemplate.RemoveLife(lifeId, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(new LifeRemoved
            {
                LifeId = lifeId,
                CampaignId = campaignId,
                CreatureTemplateId = creatureTemplate.Id
            });
        }

        public async Task UpdateLife(Guid campaignId, Guid lifeId, LifeTemplateModel life)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            creatureTemplate.UpdateLife(lifeId, life, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(new LifeUpdated
            {
                Life = life,
                CampaignId = campaignId,
                CreatureTemplateId = creatureTemplate.Id
            });
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
                .Include(template => template.Lifes)
                .FirstAsync(template => template.Id == campaign.CreatureTemplateId);
            var defenseAdded = await creatureTemplate.AddDefenseAsync(defense, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(defenseAdded);
        }

        public async Task RemoveDefense(Guid campaignId, Guid defenseId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _dbContext.CampaignTemplates
                .Include(template => template.Defenses)
                .FirstAsync(template => template.Id == campaign.CreatureTemplateId);
            var defenseRemoved = creatureTemplate.RemoveDefense(defenseId, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(defenseRemoved);
        }

        public async Task UpdateDefense(Guid campaignId, Guid defenseId, DefenseTemplateModel defense)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            var defenseUpdate = creatureTemplate.UpdateDefense(defense, _dbContext);
            _dbContext.CampaignTemplates.Update(creatureTemplate);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(defenseUpdate);
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
