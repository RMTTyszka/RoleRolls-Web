namespace RoleRollsPocketEdition.Itens.Configurations.Services;

public interface IItemConfigurationService
{
    Task<ItemConfigurationModel> GetItemConfiguration(Guid campaignId);
    Task Update(Guid campaignId, ItemConfigurationModel model);
}