using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Powers.Entities;
using System;
using System.Collections.Generic;

namespace RoleRollsPocketEdition.DefaultUniverses.Global.CombatManeuvers
{
    public class GlobalCombatManeuvers
    {
        public static PowerTemplate OpenShot => new()
        {
            Id = Guid.Parse("DA612BD0-15D3-442D-BCA0-CC94D8E8F1D6"), // OK (válido)
            Type = PowerType.Instant,
            PowerDurationType = PowerDurationType.Instant,
            Duration = null,
            ActionType = PowerActionType.AttackAction,
            Name = "Open Shot",
            Description = "",
            CastFormula = null,
            CastDescription = null,
            UseAttributeId = null,
            TargetDefenseId = null,
            UsagesFormula = null,
            UsageType = null,
            TargetType = TargetType.Self,
            Bonuses = new List<Bonus>
            {
                new Bonus
                {
                    Id = Guid.Parse("FB184927-6A5C-471D-B93D-25EC3BCA5BE9"), // OK (válido)
                    Description = null,
                    Value = 2,
                    Active = false,
                    Property = null,
                    Name = "Open Shot",
                    Application = BonusApplication.Hit,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Advantage,
                }
            },
        };

        public static PowerTemplate FullAttack => new()
        {
            Id = Guid.Parse("A0B1C2D3-E4F5-4678-9012-34567890ABCD"), // OK (válido)
            Type = PowerType.Instant,
            PowerDurationType = PowerDurationType.Instant,
            Duration = null,
            ActionType = PowerActionType.AttackAction,
            Name = "Full Attack",
            Description = "",
            CastFormula = null,
            CastDescription = null,
            UseAttributeId = null,
            TargetDefenseId = null,
            UsagesFormula = null,
            UsageType = null,
            TargetType = TargetType.Self,
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("B1C2D3E4-F5A6-4789-0123-4567890ABCDE"), // OK (válido)
                    Description = null,
                    Value = 1,
                    Active = false,
                    Property = null,
                    Name = "Full Attack Hit Advantage",
                    Application = BonusApplication.Hit,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Advantage
                },
                new Bonus
                {
                    Id = Guid.Parse("C2D3E4F5-A6B7-4890-1234-567890ABCDEF"), // OK (válido)
                    Description = null,
                    Value = 1,
                    Active = false,
                    Property = null,
                    Name = "Full Attack Evasion Disadvantage",
                    Application = BonusApplication.Hit,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Disadvantage
                },
                new Bonus
                {
                    Id = Guid.Parse("D3E4F5A6-B7C8-4901-2345-67890ABCDEF0"), // OK (válido)
                    Description = null,
                    Value = 1,
                    Active = false,
                    Property = null,
                    Name = "Full Attack Evasion Debuff",
                    Application = BonusApplication.Hit,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Debuff
                },
            ],
        };

        public static PowerTemplate PartialAttack => new()
        {
            Id = Guid.Parse("E4F5A6B7-C8D9-4012-3456-7890ABCDEF01"), // OK (válido)
            Type = PowerType.Instant,
            PowerDurationType = PowerDurationType.Instant,
            Duration = null,
            ActionType = PowerActionType.AttackAction,
            Name = "Partial Attack",
            Description = "",
            CastFormula = null,
            CastDescription = null,
            UseAttributeId = null,
            TargetDefenseId = null,
            UsagesFormula = null,
            UsageType = null,
            TargetType = TargetType.Self,
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("F5A6B7C8-D9E0-4123-4567-890ABCDEF012"), // OK (válido)
                    Description = null,
                    Value = 1,
                    Active = false,
                    Property = null,
                    Name = "Partial Attack",
                    Application = BonusApplication.Hit,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Disadvantage
                }
            ],
        };

        public static PowerTemplate CautiousAttack => new()
        {
            Id = Guid.Parse("01234567-89AB-4CDE-8012-34567890ABCD"), // Corrigido (variante "8")
            Type = PowerType.Instant,
            PowerDurationType = PowerDurationType.Instant,
            Duration = null,
            ActionType = PowerActionType.AttackAction,
            Name = "Cautious Attack",
            Description = "",
            CastFormula = null,
            CastDescription = null,
            UseAttributeId = null,
            TargetDefenseId = null,
            UsagesFormula = null,
            UsageType = null,
            TargetType = TargetType.Self,
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("12345678-9ABC-4DEF-0123-4567890ABCDE"), // OK (válido)
                    Description = null,
                    Value = 1,
                    Active = false,
                    Property = null,
                    Name = "Cautious Attack Disadvantage",
                    Application = BonusApplication.Hit,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Disadvantage
                },
                new Bonus
                {
                    Id = Guid.Parse("23456789-ABCD-4EF0-0123-4567890ABCDE"), // Corrigido (versão "4")
                    Description = null,
                    Value = 1,
                    Active = false,
                    Property = null,
                    Name = "Cautious Evasion Advantage",
                    Application = BonusApplication.Evasion,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Advantage
                },
            ],
        };

        public static PowerTemplate CoverAlly => new()
        {
            Id = Guid.Parse("34567890-ABCD-4F01-8123-4567890ABCDE"),  // OK (32 caracteres)
            Type = PowerType.Instant,
            PowerDurationType = PowerDurationType.Instant,
            Duration = null,
            ActionType = PowerActionType.AttackAction,
            Name = "Cover Ally",
            Description = "",
            CastFormula = null,
            CastDescription = null,
            UseAttributeId = null,
            TargetDefenseId = null,
            UsagesFormula = null,
            UsageType = null,
            TargetType = TargetType.Both,
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("4567890A-BCDE-4F12-9123-4567890ABCDE"),  // Removido o "F0" final
                    Description = null,
                    Value = 1,
                    Active = false,
                    Property = null,
                    Name = "Cover Ally Hit Disadvantage",
                    Application = BonusApplication.Hit,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Disadvantage,
                    Target = TargetType.Self
                },
                new Bonus
                {
                    Id = Guid.Parse("567890AB-CDEF-4F23-A123-4567890ABCDE"),  // Removido o "F0" final
                    Description = null,
                    Value = 1,
                    Active = false,
                    Property = null,
                    Name = "Cover Ally Evasion Advantage",
                    Application = BonusApplication.Evasion,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Advantage,
                    Target = TargetType.Target,
                },
            ],
        };

        public static PowerTemplate AuxiliarAttack => new()
        {
            Id = Guid.Parse("F8A23B91-D4C7-4E5B-9D8A-1234567890AB"), // OK (válido)
            Type = PowerType.Instant,
            PowerDurationType = PowerDurationType.Instant,
            Duration = null,
            ActionType = PowerActionType.AttackAction,
            Name = "Auxiliar Attack",
            Description = "",
            CastFormula = null,
            CastDescription = null,
            UseAttributeId = null,
            TargetDefenseId = null,
            UsagesFormula = null,
            UsageType = null,
            TargetType = TargetType.Both,
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("A1B2C3D4-E5F6-4A5B-9C8D-1234567890AB"), // Corrigido (completo)
                    Description = null,
                    Value = 3,
                    Active = false,
                    Property = null,
                    Name = "Auxiliar Attack Hit Disadvantage",
                    Application = BonusApplication.Hit,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Disadvantage,
                    Target = TargetType.Self
                },
                new Bonus
                {
                    Id = Guid.Parse("B2C3D4E5-F6A7-4B5C-8D9E-1234567890AB"), // Corrigido (completo)
                    Description = null,
                    Value = 2,
                    Active = false,
                    Property = null,
                    Name = "Auxiliar Attack Evasion Advantage",
                    Application = BonusApplication.Evasion,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Advantage,
                    Target = TargetType.Target,
                },
            ],
        };

        public static PowerTemplate FullCoverAlly => new()
        {
            Id = Guid.Parse("67890ABC-DEF0-4222-3456-7890ABCDEF01"), // OK (válido)
            Type = PowerType.Instant,
            PowerDurationType = PowerDurationType.Instant,
            Duration = null,
            ActionType = PowerActionType.AttackAction,
            Name = "Full Cover Ally",
            Description = "",
            CastFormula = null,
            CastDescription = null,
            UseAttributeId = null,
            TargetDefenseId = null,
            UsagesFormula = null,
            UsageType = null,
            TargetType = TargetType.Both,
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("7890ABCD-EF01-4333-4567-890ABCDEF012"), // OK (válido)
                    Description = null,
                    Value = 3,
                    Active = false,
                    Property = null,
                    Name = "Full Cover Ally Hit Disadvantage",
                    Application = BonusApplication.Hit,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Disadvantage,
                    Target = TargetType.Self
                },
                new Bonus
                {
                    Id = Guid.Parse("890ABCDE-F012-4444-5678-90ABCDEF0123"), // OK (válido)
                    Description = null,
                    Value = 2,
                    Active = false,
                    Property = null,
                    Name = "Full Cover Ally Evasion Advantage",
                    Application = BonusApplication.Evasion,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Advantage,
                    Target = TargetType.Target,
                },
            ],
        };

        public static PowerTemplate FullCover => new()
        {
            Id = Guid.Parse("90ABCDEF-0123-4555-6789-0ABCDEF01234"), // OK (válido)
            Type = PowerType.Buff,
            PowerDurationType = PowerDurationType.Turns,
            Duration = 1,
            ActionType = PowerActionType.FullAction,
            Name = "Full Defense",
            Description = "",
            CastFormula = null,
            CastDescription = null,
            UseAttributeId = null,
            TargetDefenseId = null,
            UsagesFormula = null,
            UsageType = null,
            TargetType = TargetType.Self,
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("A1B2C3D4-E5F6-4666-7890-ABCDEF012345"), // OK (válido)
                    Description = null,
                    Value = 3,
                    Active = false,
                    Property = null,
                    Name = "Full Cover Hit Disadvantage",
                    Application = BonusApplication.Hit,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Disadvantage
                },
                new Bonus
                {
                    Id = Guid.Parse("B2C3D4E5-F6A7-4777-8901-BCDEF0123456"), // OK (válido)
                    Description = null,
                    Value = 2,
                    Active = false,
                    Property = null,
                    Name = "Full Cover Evasion Advantage",
                    Application = BonusApplication.Evasion,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Advantage
                },
                new Bonus
                {
                    Id = Guid.Parse("B00BEA51-03A4-46F3-8B72-D2F30BD00CFD"), // OK (válido)
                    Description = null,
                    Value = 2,
                    Active = false,
                    Property = null,
                    Name = "Full Cover Evasion Value",
                    Application = BonusApplication.Evasion,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff
                },
            ],
        };
    }
}


