using FluentAssertions;
using RoleRollsPocketEdition.Attacks.Models;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Models;

public class AttackContractTests
{
    [Fact]
    public void EvadeInputShouldContainOnlyDefenderControlledFields()
    {
        typeof(EvadeInput).GetProperties().Select(property => property.Name).Should().BeEquivalentTo(
            "AttackerId",
            "WeaponSlot",
            "VitalityId",
            "Luck",
            "Advantage");

        typeof(EvadeResponse).GetProperty(nameof(EvadeResponse.VitalityDamage)).Should().NotBeNull();
    }

    [Fact]
    public void BasicAttackContractsShouldNotExposeSpecialAttackFields()
    {
        typeof(BasicAttackInput).GetProperty(nameof(SpecialAttackInput.SpecialSkillId)).Should().BeNull();
        typeof(BasicAttackInput).GetProperty("WeaponSlot").Should().NotBeNull();
        typeof(BasicAttackInput).GetProperty("VitalityId").Should().NotBeNull();
        typeof(BasicAttackResponse).GetProperty(nameof(BasicAttackResponse.WeaponName)).Should().NotBeNull();
        typeof(BasicAttackResponse).GetProperty(nameof(BasicAttackResponse.TotalDamage)).Should().NotBeNull();
    }

    [Fact]
    public void SpecialAttackContractsShouldNotExposeWeaponOrDamageFields()
    {
        typeof(SpecialAttackInput).GetProperty("WeaponSlot").Should().BeNull();
        typeof(SpecialAttackInput).GetProperty("VitalityId").Should().BeNull();
        typeof(SpecialAttackResponse).GetProperty(nameof(SpecialAttackResponse.SpecialSkillId)).Should().NotBeNull();
        typeof(SpecialAttackResponse).GetProperty("WeaponName").Should().BeNull();
        typeof(SpecialAttackResponse).GetProperty("TotalDamage").Should().BeNull();
        typeof(SpecialAttackResponse).GetProperty("Block").Should().BeNull();
    }
}
