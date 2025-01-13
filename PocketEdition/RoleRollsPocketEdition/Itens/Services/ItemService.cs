using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.EntityFramework;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Itens.Dtos;

namespace RoleRollsPocketEdition.Itens.Services;

public interface IItemService
{
    Task<ItemModel?> GetAsync(Guid itemId);

}

public class ItemService : IItemService, ITransientDependency
{
    private readonly RoleRollsDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public ItemService(RoleRollsDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    [NoTrackingAspect]
    public async Task<ItemModel?> GetAsync(Guid itemId)
    {
        var item = await _context.ItemInstances.SingleAsync(x => x.Id == itemId);
        return ItemModel.FromItem(item);
    }      
    
}