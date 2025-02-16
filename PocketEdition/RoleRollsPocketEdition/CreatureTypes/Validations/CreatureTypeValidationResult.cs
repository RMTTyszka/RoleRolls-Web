namespace RoleRollsPocketEdition.CreatureTypes.Validations;

public class CreatureTypeValidationResult
{
  public CreatureTypeValidation Validation { get; set; }

  public static CreatureTypeValidationResult Ok => new()
    { Validation = CreatureTypeValidation.Ok };
}

public enum CreatureTypeValidation
{
  Ok = 0
}
