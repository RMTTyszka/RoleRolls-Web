using RoleRollsPocketEdition.DefaultUniverses.TheFutureIsOutThere.CampaignTemplates.Attributes;

namespace RoleRollsPocketEdition.DefaultUniverses.TheFutureIsOutThere.CampaignTemplates.Skills;

public static class TheFutureIsOutThereSkills
{
    public static Dictionary<TheFutureIsOutThereSkill, Guid> SkillIds = new()
    {
        { TheFutureIsOutThereSkill.Combat, Guid.Parse("F84C49CA-58DC-41D2-B9A5-78E89D1B9BD6") },
        { TheFutureIsOutThereSkill.Athletics, Guid.Parse("16598015-5F8B-406A-8DEB-62A25A274936") },
        { TheFutureIsOutThereSkill.Empathy, Guid.Parse("BE25E809-3FAA-4688-9D4B-C83CE3D138A8") },
        { TheFutureIsOutThereSkill.Knowledge, Guid.Parse("F94D8DCA-47D1-4760-88DD-3FB396B21A5E") },
        { TheFutureIsOutThereSkill.Awareness, Guid.Parse("F0C4FC28-0B1A-44D6-BA36-CA43536423CE") },
        { TheFutureIsOutThereSkill.Nimbleness, Guid.Parse("DE42DF89-9B91-44DA-A1B6-F98A940CBBB3") }
    };

    public static Dictionary<TheFutureIsOutThereSkill, List<TheFutureIsOutThereMinorSkill>> SkillMinorSkills = new()
    {
        {
            TheFutureIsOutThereSkill.Combat,
            [
                TheFutureIsOutThereMinorSkill.MeleeLightWeapon,
                TheFutureIsOutThereMinorSkill.MeleeMediumWeapon,
                TheFutureIsOutThereMinorSkill.MeleeHeavyWeapon,
                TheFutureIsOutThereMinorSkill.RangedLightWeapon,
                TheFutureIsOutThereMinorSkill.RangedMediumWeapon,
                TheFutureIsOutThereMinorSkill.RangedHeavyWeapon,
                TheFutureIsOutThereMinorSkill.RobotLightControl,
                TheFutureIsOutThereMinorSkill.RobotMediumControl,
                TheFutureIsOutThereMinorSkill.RobotHeavyControl,
                TheFutureIsOutThereMinorSkill.MechaLightControl,
                TheFutureIsOutThereMinorSkill.MechaMediumControl,
                TheFutureIsOutThereMinorSkill.MechaHeavyControl,
                TheFutureIsOutThereMinorSkill.Evasion,
                TheFutureIsOutThereMinorSkill.Physique,
                TheFutureIsOutThereMinorSkill.ProcessingSpeed,
                TheFutureIsOutThereMinorSkill.MentalStrength
            ]
        }
    };

    public static Dictionary<TheFutureIsOutThereMinorSkill, Guid> MinorSkillIds = new()
    {
        { TheFutureIsOutThereMinorSkill.MeleeLightWeapon, Guid.Parse("887BA452-40D8-4BB9-B0C9-9AEEDA4BB5F2") },
        { TheFutureIsOutThereMinorSkill.MeleeMediumWeapon, Guid.Parse("AC5AAF71-C4BB-4C44-9EC7-0E94BE4A5447") },
        { TheFutureIsOutThereMinorSkill.MeleeHeavyWeapon, Guid.Parse("D7011DB0-0A9F-4C45-BB54-1E5934EFEA10") },
        { TheFutureIsOutThereMinorSkill.RangedLightWeapon, Guid.Parse("47EBE784-9D87-400A-AEF1-8C3460B47A26") },
        { TheFutureIsOutThereMinorSkill.RangedMediumWeapon, Guid.Parse("426A82A8-EDB4-48B7-B1E8-C5D131F377C7") },
        { TheFutureIsOutThereMinorSkill.RangedHeavyWeapon, Guid.Parse("8D361646-BA96-40B4-B69E-F90C3B3ABB14") },
        { TheFutureIsOutThereMinorSkill.RobotLightControl, Guid.Parse("A0E9960D-A302-48EE-BA7D-644A309F4E4E") },
        { TheFutureIsOutThereMinorSkill.RobotMediumControl, Guid.Parse("D1397C31-3A84-4E5D-AD15-02FDE9D49EB7") },
        { TheFutureIsOutThereMinorSkill.RobotHeavyControl, Guid.Parse("0A0C2107-3D60-4FBC-8CC9-96B4BEBCC8D1") },
        { TheFutureIsOutThereMinorSkill.MechaLightControl, Guid.Parse("83C18754-D8C8-43AC-B3E5-E0B858F63E39") },
        { TheFutureIsOutThereMinorSkill.MechaMediumControl, Guid.Parse("E9A0F5C4-89B0-4771-B0FD-72134D4B8EBD") },
        { TheFutureIsOutThereMinorSkill.MechaHeavyControl, Guid.Parse("E60278A9-3FBD-4328-B610-1530B027E4BD") },
        { TheFutureIsOutThereMinorSkill.Evasion, Guid.Parse("1D165D9C-760C-4CEA-96A0-5F04F40DAEB3") },
        { TheFutureIsOutThereMinorSkill.Physique, Guid.Parse("DF44BC7F-98D5-416D-861F-7A595E9D47D3") },
        { TheFutureIsOutThereMinorSkill.ProcessingSpeed, Guid.Parse("C90381F1-CF21-44EE-A24D-6CC2BD30766D") },
        { TheFutureIsOutThereMinorSkill.MentalStrength, Guid.Parse("B76B184B-4BAB-419F-AFEC-E2820EA213B1") }
    };

    public static Dictionary<TheFutureIsOutThereMinorSkill, string> MinorSkillNames = new()
    {
        { TheFutureIsOutThereMinorSkill.MeleeLightWeapon, "Melee Light Weapon" },
        { TheFutureIsOutThereMinorSkill.MeleeMediumWeapon, "Melee Medium Weapon" },
        { TheFutureIsOutThereMinorSkill.MeleeHeavyWeapon, "Melee Heavy Weapon" },
        { TheFutureIsOutThereMinorSkill.RangedLightWeapon, "Ranged Light Weapon" },
        { TheFutureIsOutThereMinorSkill.RangedMediumWeapon, "Ranged Medium Weapon" },
        { TheFutureIsOutThereMinorSkill.RangedHeavyWeapon, "Ranged Heavy Weapon" },
        { TheFutureIsOutThereMinorSkill.RobotLightControl, "Robot Light Control" },
        { TheFutureIsOutThereMinorSkill.RobotMediumControl, "Robot Medium Control" },
        { TheFutureIsOutThereMinorSkill.RobotHeavyControl, "Robot Heavy Control" },
        { TheFutureIsOutThereMinorSkill.MechaLightControl, "Mecha Light Control" },
        { TheFutureIsOutThereMinorSkill.MechaMediumControl, "Mecha Medium Control" },
        { TheFutureIsOutThereMinorSkill.MechaHeavyControl, "Mecha Heavy Control" },
        { TheFutureIsOutThereMinorSkill.Evasion, "Evasion" },
        { TheFutureIsOutThereMinorSkill.Physique, "Physique" },
        { TheFutureIsOutThereMinorSkill.ProcessingSpeed, "Processing Speed" },
        { TheFutureIsOutThereMinorSkill.MentalStrength, "Mental Strength" }
    };

    public static Dictionary<TheFutureIsOutThereMinorSkill, Guid?> MinorSkillAttributeIds = new()
    {
        { TheFutureIsOutThereMinorSkill.MeleeLightWeapon, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Agility] },
        { TheFutureIsOutThereMinorSkill.MeleeMediumWeapon, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Strength] },
        { TheFutureIsOutThereMinorSkill.MeleeHeavyWeapon, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Strength] },
        { TheFutureIsOutThereMinorSkill.RangedLightWeapon, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Agility] },
        { TheFutureIsOutThereMinorSkill.RangedMediumWeapon, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Agility] },
        { TheFutureIsOutThereMinorSkill.RangedHeavyWeapon, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Strength] },
        { TheFutureIsOutThereMinorSkill.RobotLightControl, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Intelligence] },
        { TheFutureIsOutThereMinorSkill.RobotMediumControl, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Intelligence] },
        { TheFutureIsOutThereMinorSkill.RobotHeavyControl, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Intelligence] },
        { TheFutureIsOutThereMinorSkill.MechaLightControl, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Agility] },
        { TheFutureIsOutThereMinorSkill.MechaMediumControl, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Perception] },
        { TheFutureIsOutThereMinorSkill.MechaHeavyControl, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Perception] },
        { TheFutureIsOutThereMinorSkill.Evasion, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Agility] },
        { TheFutureIsOutThereMinorSkill.Physique, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Vitality] },
        { TheFutureIsOutThereMinorSkill.ProcessingSpeed, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Perception] },
        { TheFutureIsOutThereMinorSkill.MentalStrength, TheFutureIsOutThereAttributes.AttributeIds[TheFutureIsOutThereAttribute.Intelligence] }
    };
}
