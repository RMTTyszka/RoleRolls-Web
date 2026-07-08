namespace RoleRollsPocketEdition.Attacks.Models;

public class SpecialAttackInput
{
    public Guid TargetId { get; set; }
    public Guid SpecialSkillId { get; set; }
    public Guid DefenseId { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
}
