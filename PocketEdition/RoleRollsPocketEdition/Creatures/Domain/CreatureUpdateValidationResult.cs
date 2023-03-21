namespace RoleRollsPocketEdition.Creatures.Domain;

public class CreatureUpdateValidationResult
{
    public CreatureUpdateValidation Validation { get; init; }
    public string InvalidProperty { get; init; }

    public CreatureUpdateValidationResult(CreatureUpdateValidation validation, string invalidProperty)
    {
        Validation = validation;
        InvalidProperty = invalidProperty;
    }
}