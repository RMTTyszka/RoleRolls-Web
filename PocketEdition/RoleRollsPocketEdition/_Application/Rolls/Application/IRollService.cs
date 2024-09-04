using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Domain.Rolls.Models;

namespace RoleRollsPocketEdition.Application.Rolls.Application
{
    public interface IRollService
    {
        Task<RollModel> RollAsync(Guid campaignId, Guid sceneId, Guid creatureId, RollInput input);
        Task<RollModel> RollAsync(Guid campaignId, Guid sceneId, RollInput input);
        Task<RollModel?> GetAsync(Guid campaignId, Guid sceneId, Guid id);
        Task<PagedResult<RollModel>> GetAsync(Guid campaignId, Guid sceneId, PagedRequestInput input);
    }
}