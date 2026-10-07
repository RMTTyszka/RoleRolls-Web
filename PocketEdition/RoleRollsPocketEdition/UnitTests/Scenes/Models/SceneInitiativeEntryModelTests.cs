using FluentAssertions;
using RoleRollsPocketEdition.Scenes.Entities;
using RoleRollsPocketEdition.Scenes.Models;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Scenes.Models;

public class SceneInitiativeEntryModelTests
{
    [Fact]
    public void NullDiceResultsBecomesEmptyCollection()
    {
        var entry = new SceneInitiativeEntry { DiceResults = null! };

        var model = new SceneInitiativeEntryModel(entry, "Scout");

        model.DiceResults.Should().BeEmpty();
    }
}
