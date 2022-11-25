using RoleRollsPocketEdition.Campaigns.Domain.Models;
using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;
using RoleRollsPocketEdition.Global.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Domain.Services
{
    public interface ICampaignsService
    {
        Task CreateAsync(CampaignModel campaignModel);
        Task<CampaignModel> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<PagedResult<CampaignModel>> GetListAsync(PagedRequestInput input);
        Task UpdateAsync(CampaignModel campaignModel);
        Task AddAttribute(Guid campaignId, AttributeTemplateModel attribute);
        Task RemoveAttribute(Guid campaignId, Guid attributeId);
        Task UpdateAttribute(Guid id, Guid attributeId, AttributeTemplateModel attribute);
        Task AddSkill(Guid id, Guid attributeId, SkillTemplateModel skill);
        Task RemoveSkill(Guid id, Guid attributeId, Guid skillId);
        Task UpdateSkill(Guid id, Guid attributeId, Guid skillId, SkillTemplateModel skill);     
        
        Task AddMinorSkillAsync(Guid id, Guid attributeId, Guid skillId, MinorSkillTemplateModel minorSkill);
        Task RemoveMinorSkillAsync(Guid id, Guid attributeId, Guid skillId, Guid minorSkillId);
        Task UpdateMinorSkillAsync(Guid id, Guid attributeId, Guid skillId, Guid minorSkillId, MinorSkillTemplateModel minorSkill);
    }
}
