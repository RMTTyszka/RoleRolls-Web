﻿using RoleRollsPocketEdition.Campaigns.Application.Dtos;
using RoleRollsPocketEdition.Campaigns.Domain.Models;
using RoleRollsPocketEdition.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition.Global.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Application.Services
{
    public interface ICampaignsService
    {
        Task CreateAsync(CampaignModel campaignModel);
        Task<CampaignModel> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<PagedResult<CampaignModel>> GetListAsync(PagedRequestInput input);
        Task UpdateAsync(CampaignModel campaignModel);
        Task AddAttribute(Guid campaignId, AttributeTemplateModel attribute);
        Task<List<CampaignPlayerModel>> GetPlayersAsync(Guid campaignId);
        Task RemoveAttribute(Guid campaignId, Guid attributeId);
        Task UpdateAttribute(Guid id, Guid attributeId, AttributeTemplateModel attribute);
        Task AddSkill(Guid id, Guid attributeId, SkillTemplateModel skill);
        Task RemoveSkill(Guid id, Guid attributeId, Guid skillId);
        Task UpdateSkill(Guid id, Guid attributeId, Guid skillId, SkillTemplateModel skill);     
        
        Task AddMinorSkillAsync(Guid id, Guid attributeId, Guid skillId, MinorSkillTemplateModel minorSkill);
        Task RemoveMinorSkillAsync(Guid id, Guid attributeId, Guid skillId, Guid minorSkillId);
        Task UpdateMinorSkillAsync(Guid id, Guid attributeId, Guid skillId, Guid minorSkillId, MinorSkillTemplateModel minorSkill);
        Task AddLife(Guid id, LifeTemplateModel life);
        Task RemoveLife(Guid id, Guid lifeId);
        Task UpdateLife(Guid id, Guid lifeId, LifeTemplateModel life);

        Task<ValidationResult<InvitationResult>> AcceptInvite(Guid playerId, Guid invitationCode);
        Task<Guid> Invite(Guid campaignId);
    }
}
