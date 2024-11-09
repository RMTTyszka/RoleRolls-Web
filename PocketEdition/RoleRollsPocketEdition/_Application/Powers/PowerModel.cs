using RoleRollsPocketEdition._Domain.Powers.Entities;

namespace RoleRollsPocketEdition._Application.Powers;

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