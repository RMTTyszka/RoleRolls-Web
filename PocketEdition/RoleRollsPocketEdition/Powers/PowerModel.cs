using RoleRollsPocketEdition.Powers.Entities;

namespace RoleRollsPocketEdition.Powers;

public class PowerModel
{
    public static PowerModel? FromPower(PowerTemplate? itemPower)
    {
        if (itemPower is null)
        {
            return null;
        }

        return new PowerModel();
    }
}