using System;
using System.Collections.Generic;
namespace RoleRollsPocketEdition.Itens
{
    public enum GripType
    {
        OneLightWeapon = 0,
        OneMediumWeapon = 1,
        TwoHandedHeavyWeapon = 2,
        TwoWeaponsLight = 3,
        TwoWeaponsMedium = 4,
        OneHandedHeavyWeapon = 5,
        TwoHandedMediumWeapon = 6,
        OneLightShield = 7,
        OneMediumShield = 8,
        OneHeavyShield = 9,
        None = 10
    }

    public static class GripTypeDefinition
    {
        public static readonly Dictionary<GripType, GripTypeStats> Stats;
        public static readonly Dictionary<GripType, Dictionary<GripType, GripType?>> MainGripType;

        static GripTypeDefinition()
        {
            // Initialize stats
            Stats = new Dictionary<GripType, GripTypeStats>
            {
                [GripType.OneLightWeapon] = new GripTypeStats(0, 6, 0, 2, 1, 1, 0, 0),
                [GripType.OneMediumWeapon] = new GripTypeStats(-1, 8, 1, 3, 2, 2, 0, 0),
                [GripType.TwoHandedHeavyWeapon] = new GripTypeStats(0, 12, 0, 4, 3, 3, 0, 0),
                [GripType.TwoWeaponsLight] = new GripTypeStats(0, 4, 0, 2, 2, 1, 0, 0),
                [GripType.TwoWeaponsMedium] = new GripTypeStats(1, 8, 2, 3, 2, 2, 0, 0),
                [GripType.OneHandedHeavyWeapon] = new GripTypeStats(0, 10, 0, 4, 3, 2, 0, 0),
                [GripType.TwoHandedMediumWeapon] = new GripTypeStats(2, 10, 0, 4, 4, 3, 0, 0),
                [GripType.OneLightShield] = new GripTypeStats(0, 4, 0, 2, 2, 1, 1, 0),
                [GripType.OneMediumShield] = new GripTypeStats(1, 8, 0, 3, 2, 2, 2, -1),
                [GripType.OneHeavyShield] = new GripTypeStats(3, 12, 0, 4, 4, 3, 3, -2),
                [GripType.None] = new GripTypeStats(0, 0, 0, 0, 0, 0, 0, 0)
            };

            // Initialize main grip type combinations
            MainGripType = new Dictionary<GripType, Dictionary<GripType, GripType?>>();
            
            foreach (GripType gripType in Enum.GetValues<GripType>())
            {
                MainGripType[gripType] = new Dictionary<GripType, GripType?>();
            }

            // OneLightWeapon combinations
            MainGripType[GripType.OneLightWeapon][GripType.OneLightWeapon] = GripType.TwoWeaponsLight;
            MainGripType[GripType.OneLightWeapon][GripType.OneMediumWeapon] = GripType.TwoWeaponsLight;
            MainGripType[GripType.OneLightWeapon][GripType.TwoWeaponsLight] = GripType.TwoWeaponsLight;
            MainGripType[GripType.OneLightWeapon][GripType.TwoWeaponsMedium] = GripType.TwoWeaponsLight;
            MainGripType[GripType.OneLightWeapon][GripType.TwoHandedHeavyWeapon] = null;
            MainGripType[GripType.OneLightWeapon][GripType.TwoHandedMediumWeapon] = null;
            MainGripType[GripType.OneLightWeapon][GripType.OneHandedHeavyWeapon] = null;
            MainGripType[GripType.OneLightWeapon][GripType.OneLightShield] = GripType.OneLightWeapon;
            MainGripType[GripType.OneLightWeapon][GripType.OneMediumShield] = GripType.OneLightWeapon;
            MainGripType[GripType.OneLightWeapon][GripType.OneHeavyShield] = GripType.OneLightWeapon;
            MainGripType[GripType.OneLightWeapon][GripType.None] = GripType.OneLightWeapon;

            // OneMediumWeapon combinations
            MainGripType[GripType.OneMediumWeapon][GripType.OneLightWeapon] = GripType.TwoWeaponsMedium;
            MainGripType[GripType.OneMediumWeapon][GripType.OneMediumWeapon] = GripType.TwoWeaponsMedium;
            MainGripType[GripType.OneMediumWeapon][GripType.TwoWeaponsLight] = GripType.TwoWeaponsMedium;
            MainGripType[GripType.OneMediumWeapon][GripType.TwoWeaponsMedium] = GripType.TwoWeaponsMedium;
            MainGripType[GripType.OneMediumWeapon][GripType.TwoHandedHeavyWeapon] = null;
            MainGripType[GripType.OneMediumWeapon][GripType.TwoHandedMediumWeapon] = null;
            MainGripType[GripType.OneMediumWeapon][GripType.OneHandedHeavyWeapon] = null;
            MainGripType[GripType.OneMediumWeapon][GripType.OneLightShield] = GripType.OneMediumWeapon;
            MainGripType[GripType.OneMediumWeapon][GripType.OneMediumShield] = GripType.OneMediumWeapon;
            MainGripType[GripType.OneMediumWeapon][GripType.OneHeavyShield] = GripType.OneMediumWeapon;
            MainGripType[GripType.OneMediumWeapon][GripType.None] = GripType.OneMediumWeapon;

            // TwoHandedHeavyWeapon combinations
            MainGripType[GripType.TwoHandedHeavyWeapon][GripType.OneLightWeapon] = null;
            MainGripType[GripType.TwoHandedHeavyWeapon][GripType.OneMediumWeapon] = null;
            MainGripType[GripType.TwoHandedHeavyWeapon][GripType.TwoWeaponsLight] = null;
            MainGripType[GripType.TwoHandedHeavyWeapon][GripType.TwoWeaponsMedium] = null;
            MainGripType[GripType.TwoHandedHeavyWeapon][GripType.TwoHandedHeavyWeapon] = null;
            MainGripType[GripType.TwoHandedHeavyWeapon][GripType.TwoHandedMediumWeapon] = null;
            MainGripType[GripType.TwoHandedHeavyWeapon][GripType.OneHandedHeavyWeapon] = null;
            MainGripType[GripType.TwoHandedHeavyWeapon][GripType.OneLightShield] = null;
            MainGripType[GripType.TwoHandedHeavyWeapon][GripType.OneMediumShield] = null;
            MainGripType[GripType.TwoHandedHeavyWeapon][GripType.OneHeavyShield] = null;
            MainGripType[GripType.TwoHandedHeavyWeapon][GripType.None] = GripType.TwoHandedHeavyWeapon;

            // TwoWeaponsLight combinations
            MainGripType[GripType.TwoWeaponsLight][GripType.OneLightWeapon] = GripType.TwoWeaponsLight;
            MainGripType[GripType.TwoWeaponsLight][GripType.OneMediumWeapon] = GripType.TwoWeaponsLight;
            MainGripType[GripType.TwoWeaponsLight][GripType.TwoWeaponsLight] = GripType.TwoWeaponsLight;
            MainGripType[GripType.TwoWeaponsLight][GripType.TwoWeaponsMedium] = GripType.TwoWeaponsLight;
            MainGripType[GripType.TwoWeaponsLight][GripType.TwoHandedHeavyWeapon] = null;
            MainGripType[GripType.TwoWeaponsLight][GripType.TwoHandedMediumWeapon] = null;
            MainGripType[GripType.TwoWeaponsLight][GripType.OneHandedHeavyWeapon] = null;
            MainGripType[GripType.TwoWeaponsLight][GripType.OneLightShield] = null;
            MainGripType[GripType.TwoWeaponsLight][GripType.OneMediumShield] = null;
            MainGripType[GripType.TwoWeaponsLight][GripType.OneHeavyShield] = null;
            MainGripType[GripType.TwoWeaponsLight][GripType.None] = GripType.OneLightWeapon;

            // TwoWeaponsMedium combinations
            MainGripType[GripType.TwoWeaponsMedium][GripType.OneLightWeapon] = GripType.TwoWeaponsMedium;
            MainGripType[GripType.TwoWeaponsMedium][GripType.OneMediumWeapon] = GripType.TwoWeaponsMedium;
            MainGripType[GripType.TwoWeaponsMedium][GripType.TwoWeaponsLight] = GripType.TwoWeaponsMedium;
            MainGripType[GripType.TwoWeaponsMedium][GripType.TwoWeaponsMedium] = GripType.TwoWeaponsMedium;
            MainGripType[GripType.TwoWeaponsMedium][GripType.TwoHandedHeavyWeapon] = null;
            MainGripType[GripType.TwoWeaponsMedium][GripType.TwoHandedMediumWeapon] = null;
            MainGripType[GripType.TwoWeaponsMedium][GripType.OneHandedHeavyWeapon] = null;
            MainGripType[GripType.TwoWeaponsMedium][GripType.OneLightShield] = null;
            MainGripType[GripType.TwoWeaponsMedium][GripType.OneMediumShield] = null;
            MainGripType[GripType.TwoWeaponsMedium][GripType.OneHeavyShield] = null;
            MainGripType[GripType.TwoWeaponsMedium][GripType.None] = GripType.OneMediumWeapon;

            // OneHandedHeavyWeapon combinations
            MainGripType[GripType.OneHandedHeavyWeapon][GripType.OneLightWeapon] = null;
            MainGripType[GripType.OneHandedHeavyWeapon][GripType.OneMediumWeapon] = null;
            MainGripType[GripType.OneHandedHeavyWeapon][GripType.TwoWeaponsLight] = null;
            MainGripType[GripType.OneHandedHeavyWeapon][GripType.TwoWeaponsMedium] = null;
            MainGripType[GripType.OneHandedHeavyWeapon][GripType.TwoHandedHeavyWeapon] = null;
            MainGripType[GripType.OneHandedHeavyWeapon][GripType.TwoHandedMediumWeapon] = null;
            MainGripType[GripType.OneHandedHeavyWeapon][GripType.OneHandedHeavyWeapon] = null;
            MainGripType[GripType.OneHandedHeavyWeapon][GripType.OneLightShield] = GripType.OneHandedHeavyWeapon;
            MainGripType[GripType.OneHandedHeavyWeapon][GripType.OneMediumShield] = GripType.OneHandedHeavyWeapon;
            MainGripType[GripType.OneHandedHeavyWeapon][GripType.OneHeavyShield] = GripType.OneHandedHeavyWeapon;
            MainGripType[GripType.OneHandedHeavyWeapon][GripType.None] = GripType.OneHandedHeavyWeapon;

            // OneLightShield combinations
            MainGripType[GripType.OneLightShield][GripType.OneLightWeapon] = GripType.OneLightShield;
            MainGripType[GripType.OneLightShield][GripType.OneMediumWeapon] = GripType.OneLightShield;
            MainGripType[GripType.OneLightShield][GripType.TwoWeaponsLight] = null;
            MainGripType[GripType.OneLightShield][GripType.TwoWeaponsMedium] = null;
            MainGripType[GripType.OneLightShield][GripType.TwoHandedHeavyWeapon] = null;
            MainGripType[GripType.OneLightShield][GripType.TwoHandedMediumWeapon] = null;
            MainGripType[GripType.OneLightShield][GripType.OneHandedHeavyWeapon] = GripType.OneLightShield;
            MainGripType[GripType.OneLightShield][GripType.OneLightShield] = null;
            MainGripType[GripType.OneLightShield][GripType.OneMediumShield] = null;
            MainGripType[GripType.OneLightShield][GripType.OneHeavyShield] = null;
            MainGripType[GripType.OneLightShield][GripType.None] = GripType.OneLightShield;

            // OneMediumShield combinations
            MainGripType[GripType.OneMediumShield][GripType.OneLightWeapon] = GripType.OneMediumShield;
            MainGripType[GripType.OneMediumShield][GripType.OneMediumWeapon] = GripType.OneMediumShield;
            MainGripType[GripType.OneMediumShield][GripType.TwoWeaponsLight] = null;
            MainGripType[GripType.OneMediumShield][GripType.TwoWeaponsMedium] = null;
            MainGripType[GripType.OneMediumShield][GripType.TwoHandedHeavyWeapon] = null;
            MainGripType[GripType.OneMediumShield][GripType.TwoHandedMediumWeapon] = null;
            MainGripType[GripType.OneMediumShield][GripType.OneHandedHeavyWeapon] = GripType.OneMediumShield;
            MainGripType[GripType.OneMediumShield][GripType.OneLightShield] = null;
            MainGripType[GripType.OneMediumShield][GripType.OneMediumShield] = null;
            MainGripType[GripType.OneMediumShield][GripType.OneHeavyShield] = null;
            MainGripType[GripType.OneMediumShield][GripType.None] = GripType.OneMediumShield;

            // OneHeavyShield combinations
            MainGripType[GripType.OneHeavyShield][GripType.OneLightWeapon] = GripType.OneMediumShield;
            MainGripType[GripType.OneHeavyShield][GripType.OneMediumWeapon] = GripType.OneMediumShield;
            MainGripType[GripType.OneHeavyShield][GripType.TwoWeaponsLight] = null;
            MainGripType[GripType.OneHeavyShield][GripType.TwoWeaponsMedium] = null;
            MainGripType[GripType.OneHeavyShield][GripType.TwoHandedHeavyWeapon] = null;
            MainGripType[GripType.OneHeavyShield][GripType.TwoHandedMediumWeapon] = null;
            MainGripType[GripType.OneHeavyShield][GripType.OneHandedHeavyWeapon] = GripType.OneMediumShield;
            MainGripType[GripType.OneHeavyShield][GripType.OneLightShield] = null;
            MainGripType[GripType.OneHeavyShield][GripType.OneMediumShield] = null;
            MainGripType[GripType.OneHeavyShield][GripType.OneHeavyShield] = null;
            MainGripType[GripType.OneHeavyShield][GripType.None] = GripType.OneHeavyShield;
        }

        public static int GetHit(this GripType gripType) => Stats[gripType].Hit;
        public static int GetDamage(this GripType gripType) => Stats[gripType].Damage;
        public static int GetBaseBonusDamage(this GripType gripType) => Stats[gripType].BaseBonusDamage;
        public static int GetMagicBonusModifier(this GripType gripType) => Stats[gripType].MagicBonusModifier;
        public static int GetAttributeModifier(this GripType gripType) => Stats[gripType].AttributeModifier;
        public static int GetAttackComplexity(this GripType gripType) => Stats[gripType].AttackDifficult;
        public static int GetShieldEvasionBonus(this GripType gripType) => Stats[gripType].EvasionBonus;
        public static int GetShieldHitBonus(this GripType gripType) => Stats[gripType].ShieldHitBonus;

        public static GripType? GetCombinedGripType(GripType currentGripType, GripType? otherGripType)
        {
            if (currentGripType == GripType.None)
            {
                return currentGripType;
            }

            if (otherGripType == null)
            {
                otherGripType = GripType.None;
            }

            if (MainGripType.TryGetValue(currentGripType, out var combinations) &&
                combinations.TryGetValue(otherGripType.Value, out var result))
            {
                return result;
            }

            return null;
        }

        public static GripType GetGripTypeByWeaponCategory(WeaponCategory weaponCategory)
        {
            return weaponCategory switch
            {
                WeaponCategory.None => GripType.None,
                WeaponCategory.Light => GripType.OneLightWeapon,
                WeaponCategory.Medium => GripType.OneMediumWeapon,
                WeaponCategory.Heavy => GripType.TwoHandedHeavyWeapon,
                WeaponCategory.LightShield => GripType.OneLightShield,
                WeaponCategory.MediumShield => GripType.OneMediumShield,
                WeaponCategory.HeavyShield => GripType.OneHeavyShield,
                _ => throw new ArgumentException($"Unexpected weapon category: {weaponCategory}")
            };
        }
    }

    public record GripTypeStats(
        int Hit,
        int Damage,
        int BaseBonusDamage,
        int MagicBonusModifier,
        int AttributeModifier,
        int AttackDifficult,
        int EvasionBonus,
        int ShieldHitBonus
    );
}