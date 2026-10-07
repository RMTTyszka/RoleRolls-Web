using System.Text.Json;
using RoleRollsPocketEdition.Scenes.Entities;

namespace RoleRollsPocketEdition.Scenes.Models;

public class SceneInitiativeEntryModel
{
    public SceneInitiativeEntryModel()
    {
    }

    public SceneInitiativeEntryModel(SceneInitiativeEntry entry, string creatureName)
    {
        CreatureId = entry.CreatureId;
        CreatureName = creatureName;
        Position = entry.Position;
        Score = entry.Score;
        PropertyValue = entry.PropertyValue;
        DiceResults = string.IsNullOrWhiteSpace(entry.DiceResults)
            ? []
            : JsonSerializer.Deserialize<List<int>>(entry.DiceResults) ?? [];
    }

    public Guid CreatureId { get; set; }
    public string CreatureName { get; set; } = string.Empty;
    public int Position { get; set; }
    public int Score { get; set; }
    public int PropertyValue { get; set; }
    public List<int> DiceResults { get; set; } = [];
}
