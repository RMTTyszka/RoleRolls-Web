using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Templates.Services
{
    public interface ICreatureTemplateService
    {
        public Task<CampaignTemplateModel> Get(Guid id);
        public Task Create(CampaignTemplateModel template);
        public Task<CreatureTemplateValidationResult> UpdateAsync(Guid id, CampaignTemplateModel template);
        Task<List<CampaignTemplateModel>> GetDefaults(PagedRequestInput input);
    }
}
