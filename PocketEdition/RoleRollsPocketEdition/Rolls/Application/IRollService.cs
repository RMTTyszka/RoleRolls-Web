using RoleRollsPocketEdition.Campaigns.Domain.Entities;
using RoleRollsPocketEdition.Global.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Application.Services
{
    public interface IRollService
    {
        Task<RollModel> RollAsync(Guid campaignId, Guid sceneId, Guid creatureId, RollInput input);
        Task<RollModel> RollAsync(Guid campaignId, Guid sceneId, RollInput input);
        Task<RollModel?> GetAsync(Guid campaignId, Guid sceneId, Guid id);
        Task<PagedResult<RollModel>> GetAsync(Guid campaignId, Guid sceneId, PagedRequestInput input);
    }
}