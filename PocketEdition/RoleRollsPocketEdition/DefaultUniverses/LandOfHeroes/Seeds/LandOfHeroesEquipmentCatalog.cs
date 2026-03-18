using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Templates;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.Seeds;

public static class LandOfHeroesEquipmentCatalog
{
    private const string MeleeRange = "1.5 m";
    private const string ReachRange = "3 m";

    public static IReadOnlyCollection<ItemTemplate> CreateItems(Guid campaignId) =>
    [
        CreateWeapon(
            Guid.Parse("6979BDE8-AAAD-4F52-973B-98B0CEBA435C"),
            campaignId,
            "Club",
            WeaponCategory.Light,
            WeaponDamageType.Bludgeoning,
            range: MeleeRange),
        CreateWeapon(
            Guid.Parse("77FB160D-B3FC-45A6-9EC4-2CD980A33D1B"),
            campaignId,
            "Dagger",
            WeaponCategory.Light,
            WeaponDamageType.Piercing,
            range: MeleeRange),
        CreateWeapon(
            Guid.Parse("C26D63A0-85D7-46F3-BCD6-F6EA473BEB5F"),
            campaignId,
            "Handaxe",
            WeaponCategory.Light,
            WeaponDamageType.Cutting,
            range: MeleeRange),
        CreateWeapon(
            Guid.Parse("6164F0E2-7D4C-40B5-8823-F10CB01E0AAC"),
            campaignId,
            "Shortsword",
            WeaponCategory.Light,
            WeaponDamageType.Piercing,
            range: MeleeRange),
        CreateWeapon(
            Guid.Parse("7BEF7D35-2669-4B30-8B81-91B5F25F2B4C"),
            campaignId,
            "Scimitar",
            WeaponCategory.Light,
            WeaponDamageType.Cutting,
            range: MeleeRange),
        CreateWeapon(
            Guid.Parse("5E54ADF2-D61A-4585-8309-3290BC9A5206"),
            campaignId,
            "Rapier",
            WeaponCategory.Medium,
            WeaponDamageType.Piercing,
            range: MeleeRange),
        CreateWeapon(
            Guid.Parse("F2A76249-3131-4C14-B746-B93044B8436C"),
            campaignId,
            "Longsword",
            WeaponCategory.Medium,
            WeaponDamageType.Cutting,
            range: MeleeRange),
        CreateWeapon(
            Guid.Parse("474F40FA-9EAB-42D9-94B2-D1DB1B4237E9"),
            campaignId,
            "Battleaxe",
            WeaponCategory.Medium,
            WeaponDamageType.Cutting,
            range: MeleeRange),
        CreateWeapon(
            Guid.Parse("6B2CED5B-EFC7-45E0-83EF-18CAF4B6D54F"),
            campaignId,
            "Warhammer",
            WeaponCategory.Medium,
            WeaponDamageType.Bludgeoning,
            range: MeleeRange),
        CreateWeapon(
            Guid.Parse("97AC2A80-0FDF-4661-BD1F-DB5821792D7F"),
            campaignId,
            "Spear",
            WeaponCategory.Medium,
            WeaponDamageType.Piercing,
            range: ReachRange),
        CreateWeapon(
            Guid.Parse("D7CC08F8-9BCB-4DA3-95D8-A7050804C045"),
            campaignId,
            "Quarterstaff",
            WeaponCategory.Medium,
            WeaponDamageType.Bludgeoning,
            range: ReachRange),
        CreateWeapon(
            Guid.Parse("351B2C0A-FAA5-4311-8686-F1E0B376F16A"),
            campaignId,
            "Greatsword",
            WeaponCategory.Heavy,
            WeaponDamageType.Cutting,
            range: MeleeRange),
        CreateWeapon(
            Guid.Parse("4000022A-817A-4ACD-AB92-5BFC3B67CD2A"),
            campaignId,
            "Greataxe",
            WeaponCategory.Heavy,
            WeaponDamageType.Cutting,
            range: MeleeRange),
        CreateWeapon(
            Guid.Parse("FF96C99D-50B0-4216-96D0-44CFDD2677E4"),
            campaignId,
            "Maul",
            WeaponCategory.Heavy,
            WeaponDamageType.Bludgeoning,
            range: MeleeRange),
        CreateWeapon(
            Guid.Parse("9905F405-EF4A-4411-B83E-AD886FE8B886"),
            campaignId,
            "Shortbow",
            WeaponCategory.Light,
            WeaponDamageType.Piercing,
            isRanged: true,
            range: "24/96 m"),
        CreateWeapon(
            Guid.Parse("5B7FF31B-0AD2-434A-8701-200F332CFCDF"),
            campaignId,
            "Longbow",
            WeaponCategory.Heavy,
            WeaponDamageType.Piercing,
            isRanged: true,
            range: "45/180 m"),
        CreateWeapon(
            Guid.Parse("1141C0C4-09DE-4509-9A82-3281C9D77735"),
            campaignId,
            "Light Crossbow",
            WeaponCategory.Medium,
            WeaponDamageType.Piercing,
            isRanged: true,
            range: "24/96 m"),
        CreateWeapon(
            Guid.Parse("68D2AB20-F976-40B1-8F50-3F51D57ADE2E"),
            campaignId,
            "Heavy Crossbow",
            WeaponCategory.Heavy,
            WeaponDamageType.Piercing,
            isRanged: true,
            range: "30/120 m"),
        CreateWeapon(
            Guid.Parse("4D21D58A-B2BB-44B6-9225-AEE1DF4D7D46"),
            campaignId,
            "Buckler",
            WeaponCategory.LightShield,
            WeaponDamageType.Shield,
            range: MeleeRange,
            slot: EquipableSlot.OffHand),
        CreateWeapon(
            Guid.Parse("6DBE1EB3-0B71-4EC5-846E-D36E5F4779D2"),
            campaignId,
            "Shield",
            WeaponCategory.MediumShield,
            WeaponDamageType.Shield,
            range: MeleeRange,
            slot: EquipableSlot.OffHand),
        CreateWeapon(
            Guid.Parse("DE9F35EA-C362-4753-8D53-A29650535C67"),
            campaignId,
            "Tower Shield",
            WeaponCategory.HeavyShield,
            WeaponDamageType.Shield,
            range: MeleeRange,
            slot: EquipableSlot.OffHand),
        CreateArmor(
            Guid.Parse("4F0191D9-9FBB-48B6-BFB6-684394CC5F03"),
            campaignId,
            "Padded Armor",
            ArmorCategory.Light),
        CreateArmor(
            Guid.Parse("4D99B8EF-7E30-4363-BB13-880A664B099F"),
            campaignId,
            "Leather Armor",
            ArmorCategory.Light),
        CreateArmor(
            Guid.Parse("64563A9A-1E60-42B9-B6D4-8DA82DC7F07D"),
            campaignId,
            "Studded Leather",
            ArmorCategory.Light),
        CreateArmor(
            Guid.Parse("CBE27BCF-85C9-48F1-A514-720C133D75F9"),
            campaignId,
            "Chain Shirt",
            ArmorCategory.Medium),
        CreateArmor(
            Guid.Parse("59FD3225-C226-4AAD-8C98-7DA8BACEC40C"),
            campaignId,
            "Scale Mail",
            ArmorCategory.Medium),
        CreateArmor(
            Guid.Parse("6F6AA37A-670B-44C6-A058-3E206DB1C2F9"),
            campaignId,
            "Breastplate",
            ArmorCategory.Medium),
        CreateArmor(
            Guid.Parse("3AEFE021-B626-451F-902F-BE6D352B0A65"),
            campaignId,
            "Half Plate",
            ArmorCategory.Medium),
        CreateArmor(
            Guid.Parse("6A0D3FC6-EE9A-4B6B-9955-2E8D4193F972"),
            campaignId,
            "Chain Mail",
            ArmorCategory.Heavy),
        CreateArmor(
            Guid.Parse("F4BCE0B0-42CB-46CF-A01E-73D4454C029E"),
            campaignId,
            "Splint Armor",
            ArmorCategory.Heavy),
        CreateArmor(
            Guid.Parse("360A905C-D172-40D6-BACD-8A62AC93A185"),
            campaignId,
            "Plate Armor",
            ArmorCategory.Heavy)
    ];

    private static WeaponTemplate CreateWeapon(
        Guid id,
        Guid campaignId,
        string name,
        WeaponCategory category,
        WeaponDamageType damageType,
        bool isRanged = false,
        string? range = null,
        EquipableSlot slot = EquipableSlot.MainHand)
    {
        return new WeaponTemplate
        {
            Id = id,
            CampaignId = campaignId,
            Name = name,
            Type = ItemType.Weapon,
            Slot = slot,
            Category = category,
            DamageType = damageType,
            IsRanged = isRanged,
            Range = range
        };
    }

    private static ArmorTemplate CreateArmor(Guid id, Guid campaignId, string name, ArmorCategory category)
    {
        return new ArmorTemplate
        {
            Id = id,
            CampaignId = campaignId,
            Name = name,
            Type = ItemType.Armor,
            Slot = EquipableSlot.Chest,
            Category = category
        };
    }
}
