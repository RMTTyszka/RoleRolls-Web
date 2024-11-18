using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition._Application.Itens.Configurations.Services;
using RoleRollsPocketEdition._Domain.Itens.Configurations;

namespace RoleRollsPocketEdition._Application.Itens.Configurations.Controllers;
[Route("campaigns/{campaignId:guid}/item-configurations")]
public class ItemConfigurationController : ControllerBase
{
    private readonly IItemConfigurationService _iItemConfigurationService;

    public ItemConfigurationController(IItemConfigurationService iItemConfigurationService)
    {
        _iItemConfigurationService = iItemConfigurationService;
    }
    
    [HttpGet]
    public async Task<ItemConfigurationModel> GetItemConfiguration([FromRoute] Guid campaignId)
    {
        return await _iItemConfigurationService.GetItemConfiguration(campaignId);
    }   
    [HttpPut]
    public async Task Update([FromRoute] Guid campaignId, [FromBody] ItemConfigurationModel model)
    {
        await _iItemConfigurationService.Update(campaignId, model);
    }
}