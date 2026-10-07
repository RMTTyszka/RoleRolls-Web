using RoleRollsPocketEdition.Creatures.Entities;

namespace RoleRollsPocketEdition.Creatures;

public class CreatureUpdateValidationResult
{
    public CreatureUpdateValidation Validation { get; init; }
    public string? InvalidProperty { get; init; }

    public Creature Creature { get; set; } = null!;

    public CreatureUpdateValidationResult(CreatureUpdateValidation validation, string? invalidProperty)
    {
        Validation = validation;
        InvalidProperty = invalidProperty;
    }
}
