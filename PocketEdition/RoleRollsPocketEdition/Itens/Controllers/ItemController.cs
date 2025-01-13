using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Itens.Dtos;
using RoleRollsPocketEdition.Itens.Services;

namespace RoleRollsPocketEdition.Itens.Controllers;

[Route("itens")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet("{id}")]
    public async Task<ItemModel?> GetItemAsync(Guid id)
    {
        return await _itemService.GetAsync(id);

    }
}