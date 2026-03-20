namespace RoleRollsPocketEdition.Scenes.Models;

public class SceneBoardDocument
{
    public Guid SceneId { get; set; }
    public int Version { get; set; }
    public SceneBoardViewport Viewport { get; set; } = new();
    public List<SceneBoardStroke> Strokes { get; set; } = [];
    public List<SceneBoardToken> Tokens { get; set; } = [];
    public string? BackgroundUrl { get; set; }

    public static SceneBoardDocument CreateEmpty(Guid sceneId)
    {
        return new SceneBoardDocument
        {
            SceneId = sceneId,
            Version = 0,
            Viewport = new SceneBoardViewport(),
            Strokes = [],
            Tokens = [],
            BackgroundUrl = null
        };
    }
}

public class SceneBoardState
{
    public SceneBoardViewport Viewport { get; set; } = new();
    public List<SceneBoardStroke> Strokes { get; set; } = [];
    public List<SceneBoardToken> Tokens { get; set; } = [];
    public string? BackgroundUrl { get; set; }

    public static SceneBoardState CreateEmpty()
    {
        return new SceneBoardState
        {
            Viewport = new SceneBoardViewport(),
            Strokes = [],
            Tokens = [],
            BackgroundUrl = null
        };
    }
}

public class SceneBoardViewport
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Scale { get; set; } = 1;
}

public class SceneBoardStroke
{
    public Guid Id { get; set; }
    public string Color { get; set; } = string.Empty;
    public int Width { get; set; }
    public List<double> Points { get; set; } = [];
    public string CreatedAt { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
}

public class SceneBoardToken
{
    public Guid Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public string Color { get; set; } = string.Empty;
    public int ZIndex { get; set; }
    public string? ImageUrl { get; set; }
    public Guid? CreatureId { get; set; }
    public bool Locked { get; set; }
}
