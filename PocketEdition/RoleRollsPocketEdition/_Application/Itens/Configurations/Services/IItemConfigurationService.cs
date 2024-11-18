using RoleRollsPocketEdition._Domain.Itens.Configurations;

namespace RoleRollsPocketEdition._Application.Itens.Configurations.Services;

public interface IItemConfigurationService
{
    Task<ItemConfigurationModel> GetItemConfiguration(Guid campaignId);
    Task Update(Guid campaignId, ItemConfigurationModel model);
}