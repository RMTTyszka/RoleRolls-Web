using RoleRollsPocketEdition.Powers.Entities;

namespace RoleRollsPocketEdition.Powers;

public class PowerInstanceModel
{
    public static PowerInstanceModel? FromPower(PowerInstance? itemPower)
    {
        if (itemPower is null)
        {
            return null;
        }

        return new PowerInstanceModel();
    }
}