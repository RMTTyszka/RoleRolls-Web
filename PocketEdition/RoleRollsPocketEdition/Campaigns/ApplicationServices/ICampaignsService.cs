using RoleRollsPocketEdition.Campaigns.Dtos;
using RoleRollsPocketEdition.Campaigns.Models;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.ApplicationServices
{
    public interface ICampaignsService
    {
        Task CreateAsync(CampaignCreateInput campaignModel);
        Task<CampaignModel> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<PagedResult<CampainView>> GetListAsync(PagedRequestInput input);
        Task UpdateAsync(Guid id, CampaignUpdateInput campaignModel);
        Task AddAttribute(Guid campaignId, AttributeTemplateModel attribute);
        Task<List<CampaignPlayerModel>> GetPlayersAsync(Guid campaignId);
        Task RemoveAttribute(Guid campaignId, Guid attributeId);
        Task UpdateAttribute(Guid id, Guid attributeId, AttributeTemplateModel attribute);
        Task AddSkill(Guid id, Guid? attributeId, SkillTemplateModel skill);
        Task RemoveSkill(Guid id, Guid? attributeId, Guid skillId);
        Task UpdateSkill(Guid id, Guid? attributeId, Guid skillId, SkillTemplateModel skill);     
        
        Task AddMinorSkillAsync(Guid id, Guid? attributeId, Guid skillId, SpecificSkillTemplateModel specificSkill);
        Task RemoveMinorSkillAsync(Guid id, Guid? attributeId, Guid skillId, Guid minorSkillId);
        Task UpdateMinorSkillAsync(Guid id, Guid? attributeId, Guid skillId, Guid minorSkillId, SpecificSkillTemplateModel specificSkill);
        Task AddVitality(Guid id, VitalityTemplateModel vitality);
        Task RemoveVitality(Guid id, Guid vitalityId);
        Task UpdateVitality(Guid id, Guid vitalityId, VitalityTemplateModel vitality);

        Task<ValidationResult<InvitationResult>> AcceptInvite(Guid playerId, Guid invitationCode);
        Task<Guid> Invite(Guid campaignId);
        Task AddDefense(Guid campaignId, DefenseTemplateModel defense);
        Task RemoveDefense(Guid campaignId, Guid defenseId);
        Task UpdateDefense(Guid campaignId, Guid defenseId, DefenseTemplateModel defense);
    }
}
