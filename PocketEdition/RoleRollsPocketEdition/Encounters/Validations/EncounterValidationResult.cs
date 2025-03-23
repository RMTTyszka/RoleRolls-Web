namespace RoleRollsPocketEdition.Encounters.Validations;

public class EncounterValidationResult
{
    public EncounterValidationMotive Motive { get; set; }

    public static EncounterValidationResult NotFound()
    {
        return new EncounterValidationResult
        {
            Motive = EncounterValidationMotive.NotFound
        };
    }   
    public static EncounterValidationResult Ok()
    {
        return new EncounterValidationResult
        {
            Motive = EncounterValidationMotive.Ok
        };
    }
    public bool Success => Motive == EncounterValidationMotive.Ok;
}
public enum EncounterValidationMotive
{
    Ok = 0,
    NotFound = 1
}