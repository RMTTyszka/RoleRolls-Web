namespace RoleRollsPocketEdition.Scenes.Models;

public static class SceneBoardOperationKinds
{
    public const string StrokeAdded = "stroke-added";
    public const string StrokeRemoved = "stroke-removed";
    public const string TokenUpserted = "token-upserted";
    public const string TokenMoved = "token-moved";
    public const string TokenRenamed = "token-renamed";
    public const string TokenLockChanged = "token-lock-changed";
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

public class BoardTokenMoveInput
{
    public double X { get; set; }
    public double Y { get; set; }
}

public class BoardTokenRenameInput
{
    public string Label { get; set; } = string.Empty;
}

public class BoardTokenLockInput
{
    public bool Locked { get; set; }
}

public class BoardTokenMovedPayload
{
    public Guid TokenId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
}

public class BoardTokenRenamedPayload
{
    public Guid TokenId { get; set; }
    public string Label { get; set; } = string.Empty;
}

public class BoardTokenLockChangedPayload
{
    public Guid TokenId { get; set; }
    public bool Locked { get; set; }
}
