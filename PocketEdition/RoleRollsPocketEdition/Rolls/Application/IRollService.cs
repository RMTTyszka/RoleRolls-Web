using RoleRollsPocketEdition.Campaigns.Domain.Entities;
using RoleRollsPocketEdition.Global.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Application.Services
{
    public interface IRollService
    {
        Task<RollModel> RollAsync(Guid campaignId, Guid creatureId, RollInput input);
        Task<RollModel> RollAsync(Guid campaignId, RollInput input);
        Task<RollModel?> GetAsync(Guid campaignId, Guid id);
        Task<PagedResult<RollModel>> GetAsync(Guid campaignId, PagedRequestInput input);
    }
}