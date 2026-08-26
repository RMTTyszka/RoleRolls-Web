using FluentAssertions;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.Templates.Entities;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Creatures.Entities;

public class CreatureFormulaTests
{
    [Theory]
    [InlineData("5 / 2", 2)]
    [InlineData("3 / 2", 1)]
    [InlineData("-6 / 5", -2)]
    public void ApplyFormula_ShouldRoundFractionalResultsDown(string formula, int expectedValue)
    {
        var creature = new BaseCreature(LandOfHeroesTemplate.Template, "").Creature;
        var formulaTokens = new[]
        {
            new FormulaToken
            {
                Type = FormulaTokenType.Manual,
                ManualValue = formula
            }
        };

        creature.ApplyFormula(string.Empty, formulaTokens).Should().Be(expectedValue);
    }
}
