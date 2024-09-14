using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Application.Itens.Services;
using RoleRollsPocketEdition._Domain.Itens.Templates.Models;
using RoleRollsPocketEdition.Core.Dtos;

namespace RoleRollsPocketEdition._Application.Itens.Controllers;

[Route("item-templates")]
public class ItemTemplateController : ControllerBase
{
    private readonly IItemTemplateService _itemTemplateService;

    public ItemTemplateController(IItemTemplateService itemTemplateService)
    {
        _itemTemplateService = itemTemplateService;
    }

    [HttpGet("")]
    public async Task<PagedResult<object>> GetItemsAsync([FromQuery] Guid? campaignId, [FromQuery] GetAllItensTemplateInput input)
    {
        return await _itemTemplateService.GetItemsAsync(campaignId, input);
    }    
    [HttpGet("{id}")]
    public async Task<ItemTemplateModel> GetItemAsync(Guid id)
    {
        return await _itemTemplateService.GetItemAsync(id);

    }

    [HttpPost("")]
    public async Task InsertItem([FromBody] ItemTemplateModel item)
    {
        await _itemTemplateService.InsertItem(item);
    }    
    [HttpPut("{id}")]
    public async Task UpdateItem([FromRoute] Guid id, [FromBody] ItemTemplateModel item)
    {
        await _itemTemplateService.UpdateItem(id, item);

    }   
    [HttpDelete("{id}")]
    public async Task DeleteItem([FromRoute] Guid id)
    {
        await _itemTemplateService.DeleteItem(id);
    }
}