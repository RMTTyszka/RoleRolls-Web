namespace RoleRollsPocketEdition.Scenes.Models;

public static class SceneBoardOperationKinds
{
    public const string StrokeAdded = "stroke-added";
    public const string StrokeRemoved = "stroke-removed";
    public const string TokenUpserted = "token-upserted";
    public const string TokenRemoved = "token-removed";
    public const string BoardCleared = "board-cleared";
}

public class BoardCommand<TPayload>
{
    public string OpId { get; set; } = string.Empty;
    public TPayload Payload { get; set; } = default!;
}

public class BoardDeleteCommand
{
    public string OpId { get; set; } = string.Empty;
}

public class BoardOperationEnvelope
{
    public Guid SceneId { get; set; }
    public int Version { get; set; }
    public string OpId { get; set; } = string.Empty;
    public string Kind { get; set; } = string.Empty;
    public object? Payload { get; set; }
}

public class BoardStrokeRemovedPayload
{
    public Guid StrokeId { get; set; }
}

public class BoardTokenRemovedPayload
{
    public Guid TokenId { get; set; }
}
