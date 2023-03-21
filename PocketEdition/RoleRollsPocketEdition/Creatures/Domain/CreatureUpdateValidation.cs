﻿namespace RoleRollsPocketEdition.Creatures.Domain;

public enum CreatureUpdateValidation
{
    Ok = 0,
    AttributePointsGreaterThanAllowed = 1,
    SkillPointsGreaterThanAllowed = 2,
    InvalidModel = 3,
}