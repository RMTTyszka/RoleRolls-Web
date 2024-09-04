using RoleRollsPocketEdition.Application.Creatures.Models;

namespace RoleRollsPocketEdition.Application.Creatures;

public class CreatureUpdateValidationResult
{
    public CreatureUpdateValidation Validation { get; init; }
    public string InvalidProperty { get; init; }
    
    public CreatureModel Creature { get; set; }

    public CreatureUpdateValidationResult(CreatureUpdateValidation validation, string invalidProperty)
    {
        Validation = validation;
        InvalidProperty = invalidProperty;
    }
}