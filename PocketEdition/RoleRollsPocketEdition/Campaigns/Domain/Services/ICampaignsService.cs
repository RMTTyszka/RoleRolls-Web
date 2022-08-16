using RoleRollsPocketEdition.Campaigns.Domain.Models;
using RoleRollsPocketEdition.Global.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Domain.Services
{
    public interface ICampaignsService
    {
        Task CreateAsync(CampaignModel campaignModel);
        Task<CampaignModel> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<PagedResult<CampaignModel>> GetListAsync(PagedRequestInput input);
    }
}
