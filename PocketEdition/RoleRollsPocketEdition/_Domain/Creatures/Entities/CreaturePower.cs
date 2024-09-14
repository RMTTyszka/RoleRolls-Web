using RoleRollsPocketEdition._Domain.Powers.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Creatures.Entities;

public class CreaturePower : Entity
{
    public Creature Creature { get; set; }
    public Guid CreatureId { get; set; }
    public PowerTemplate PowerTemplate { get; set; }
    public Guid PowerTemplateId { get; set; }
    public int ConsumedUsages { get; set; }


    public void Refresh()
    {
        ConsumedUsages = 0;
    }

    public int MaxUsages()
    {
        return Creature.ApplyFormula(PowerTemplate.UsagesFormula);
    }

    public void GainUsages(int usages)
    {
        ConsumedUsages += usages;
        ConsumedUsages = Math.Min(ConsumedUsages, Creature.ApplyFormula(PowerTemplate.UsagesFormula));
    }

    public void LoseUsages(int usagesLost)
    {
        ConsumedUsages -= usagesLost;
        ConsumedUsages = Math.Max(ConsumedUsages, 0);
    }
}