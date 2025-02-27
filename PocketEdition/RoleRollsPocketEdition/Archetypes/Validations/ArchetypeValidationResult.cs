namespace RoleRollsPocketEdition.Archetypes.Validations;

public class ArchetypeValidationResult
{
  public ArchetypeValidation Validation { get; set; }

  public static ArchetypeValidationResult Ok => new()
    { Validation = ArchetypeValidation.Ok };
}

public enum ArchetypeValidation
{
  Ok = 0
}
