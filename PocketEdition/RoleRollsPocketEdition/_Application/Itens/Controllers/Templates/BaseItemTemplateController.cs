using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Application.Itens.Services;
using RoleRollsPocketEdition._Domain.Itens.Templates;
using RoleRollsPocketEdition._Domain.Itens.Templates.Models;
using RoleRollsPocketEdition.Core.Dtos;

namespace RoleRollsPocketEdition._Application.Itens.Controllers;

public class BaseItemTemplateController<T, TEntity> : ControllerBase where T : ItemTemplateModel, new()
where TEntity : ItemTemplate
{
    protected readonly IItemTemplateService _itemTemplateService;

    public BaseItemTemplateController(IItemTemplateService itemTemplateService)
    {
        _itemTemplateService = itemTemplateService;
    }

    [HttpGet("")]
    public async Task<PagedResult<T>> GetItemsAsync([FromQuery] Guid? campaignId, [FromQuery] GetAllItensTemplateInput input)
    {
        return await _itemTemplateService.GetItemsAsync<T, TEntity>(campaignId, input);
    }    
    [HttpGet("{id}")]
    public async Task<ItemTemplateModel> GetItemAsync(Guid id)
    {
        return await _itemTemplateService.GetItemAsync(id);

    }
    
}