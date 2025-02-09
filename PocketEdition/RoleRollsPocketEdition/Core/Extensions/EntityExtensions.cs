using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Core.Extensions;

public class SyncResult<T, TModel>
{
    public List<TModel> ToAdd { get; set; } = new();
    public List<TModel> ToUpdate { get; set; } = new();
    public List<T> ToRemove { get; set; } = new();
}

public static class EntityExtensions
{
    public static SyncResult<T, TModel> Synchronize<T, TModel>(this List<T> existingEntities, List<TModel> newModels)
        where T : Entity
        where TModel : IEntityDto
    {
        var result = new SyncResult<T, TModel>();

        var existingDict = existingEntities.ToDictionary(e => e.Id);
        var newDict = newModels.ToDictionary(m => m.Id);

        foreach (var newModel in newModels)
        {
            if (!existingDict.ContainsKey(newModel.Id))
            {
                result.ToAdd.Add(newModel);
            }
            else
            {
                result.ToUpdate.Add(newModel);
            }
        }

        foreach (var existingEntity in existingEntities)
        {
            if (!newDict.ContainsKey(existingEntity.Id))
            {
                result.ToRemove.Add(existingEntity);
            }
        }

        return result;
    }
}

public interface IEntityDto
{
    public Guid Id { get; set; }
}