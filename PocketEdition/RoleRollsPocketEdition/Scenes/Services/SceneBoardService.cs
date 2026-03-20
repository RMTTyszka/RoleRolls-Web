using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.Authentication.Application.Services;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Scenes.Entities;
using RoleRollsPocketEdition.Scenes.Models;

namespace RoleRollsPocketEdition.Scenes.Services;

public interface ISceneBoardService
{
    Task<SceneBoardDocument?> GetAsync(Guid campaignId, Guid sceneId);
    Task<BoardOperationEnvelope?> AddStrokeAsync(Guid campaignId, Guid sceneId, BoardCommand<SceneBoardStroke> command);
    Task<BoardOperationEnvelope?> RemoveStrokeAsync(Guid campaignId, Guid sceneId, Guid strokeId, BoardDeleteCommand command);
    Task<BoardOperationEnvelope?> UpsertTokenAsync(Guid campaignId, Guid sceneId, Guid tokenId, BoardCommand<SceneBoardToken> command);
    Task<BoardOperationEnvelope?> RemoveTokenAsync(Guid campaignId, Guid sceneId, Guid tokenId, BoardDeleteCommand command);
    Task<BoardOperationEnvelope?> ClearAsync(Guid campaignId, Guid sceneId, BoardDeleteCommand command);
}

public class SceneBoardService : ISceneBoardService, ITransientDependency
{
    private readonly RoleRollsDbContext _dbContext;
    private readonly ISceneNotificationService _sceneNotificationService;
    private readonly ICurrentUser _currentUser;

    public SceneBoardService(
        RoleRollsDbContext dbContext,
        ISceneNotificationService sceneNotificationService,
        ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _sceneNotificationService = sceneNotificationService;
        _currentUser = currentUser;
    }

    public async Task<SceneBoardDocument?> GetAsync(Guid campaignId, Guid sceneId)
    {
        if (!await SceneExistsAsync(campaignId, sceneId))
        {
            return null;
        }

        var board = await _dbContext.SceneBoards
            .AsNoTracking()
            .SingleOrDefaultAsync(currentBoard => currentBoard.SceneId == sceneId);

        return board is null
            ? SceneBoardDocument.CreateEmpty(sceneId)
            : ToDocument(board);
    }

    public async Task<BoardOperationEnvelope?> AddStrokeAsync(
        Guid campaignId,
        Guid sceneId,
        BoardCommand<SceneBoardStroke> command)
    {
        var board = await GetOrCreateBoardAsync(campaignId, sceneId);
        if (board is null)
        {
            return null;
        }

        var stroke = CloneStroke(command.Payload);
        board.State.Strokes = board.State.Strokes
            .Where(existingStroke => existingStroke.Id != stroke.Id)
            .Append(stroke)
            .ToList();

        return await PersistOperationAsync(
            board,
            command.OpId,
            SceneBoardOperationKinds.StrokeAdded,
            stroke);
    }

    public async Task<BoardOperationEnvelope?> RemoveStrokeAsync(
        Guid campaignId,
        Guid sceneId,
        Guid strokeId,
        BoardDeleteCommand command)
    {
        if (!await SceneExistsAsync(campaignId, sceneId))
        {
            return null;
        }

        var board = await _dbContext.SceneBoards.SingleOrDefaultAsync(currentBoard => currentBoard.SceneId == sceneId);
        if (board is null)
        {
            return BuildEnvelope(
                sceneId,
                0,
                command.OpId,
                SceneBoardOperationKinds.StrokeRemoved,
                new BoardStrokeRemovedPayload { StrokeId = strokeId });
        }

        var removed = board.State.Strokes.RemoveAll(stroke => stroke.Id == strokeId) > 0;
        if (!removed)
        {
            return BuildEnvelope(
                sceneId,
                board.Version,
                command.OpId,
                SceneBoardOperationKinds.StrokeRemoved,
                new BoardStrokeRemovedPayload { StrokeId = strokeId });
        }

        return await PersistOperationAsync(
            board,
            command.OpId,
            SceneBoardOperationKinds.StrokeRemoved,
            new BoardStrokeRemovedPayload { StrokeId = strokeId });
    }

    public async Task<BoardOperationEnvelope?> UpsertTokenAsync(
        Guid campaignId,
        Guid sceneId,
        Guid tokenId,
        BoardCommand<SceneBoardToken> command)
    {
        var board = await GetOrCreateBoardAsync(campaignId, sceneId);
        if (board is null)
        {
            return null;
        }

        var token = CloneToken(command.Payload);
        token.Id = tokenId;

        var existingIndex = board.State.Tokens.FindIndex(currentToken => currentToken.Id == token.Id);
        if (existingIndex >= 0)
        {
            board.State.Tokens[existingIndex] = token;
        }
        else
        {
            board.State.Tokens.Add(token);
        }

        board.State.Tokens = board.State.Tokens
            .OrderBy(currentToken => currentToken.ZIndex)
            .ToList();

        return await PersistOperationAsync(
            board,
            command.OpId,
            SceneBoardOperationKinds.TokenUpserted,
            token);
    }

    public async Task<BoardOperationEnvelope?> RemoveTokenAsync(
        Guid campaignId,
        Guid sceneId,
        Guid tokenId,
        BoardDeleteCommand command)
    {
        if (!await SceneExistsAsync(campaignId, sceneId))
        {
            return null;
        }

        var board = await _dbContext.SceneBoards.SingleOrDefaultAsync(currentBoard => currentBoard.SceneId == sceneId);
        if (board is null)
        {
            return BuildEnvelope(
                sceneId,
                0,
                command.OpId,
                SceneBoardOperationKinds.TokenRemoved,
                new BoardTokenRemovedPayload { TokenId = tokenId });
        }

        var removed = board.State.Tokens.RemoveAll(token => token.Id == tokenId) > 0;
        if (!removed)
        {
            return BuildEnvelope(
                sceneId,
                board.Version,
                command.OpId,
                SceneBoardOperationKinds.TokenRemoved,
                new BoardTokenRemovedPayload { TokenId = tokenId });
        }

        return await PersistOperationAsync(
            board,
            command.OpId,
            SceneBoardOperationKinds.TokenRemoved,
            new BoardTokenRemovedPayload { TokenId = tokenId });
    }

    public async Task<BoardOperationEnvelope?> ClearAsync(Guid campaignId, Guid sceneId, BoardDeleteCommand command)
    {
        if (!await SceneExistsAsync(campaignId, sceneId))
        {
            return null;
        }

        var board = await _dbContext.SceneBoards.SingleOrDefaultAsync(currentBoard => currentBoard.SceneId == sceneId);
        if (board is null)
        {
            return BuildEnvelope(sceneId, 0, command.OpId, SceneBoardOperationKinds.BoardCleared, null);
        }

        if (board.State.Strokes.Count == 0 && board.State.Tokens.Count == 0)
        {
            return BuildEnvelope(sceneId, board.Version, command.OpId, SceneBoardOperationKinds.BoardCleared, null);
        }

        board.State.Strokes = [];
        board.State.Tokens = [];

        return await PersistOperationAsync(
            board,
            command.OpId,
            SceneBoardOperationKinds.BoardCleared,
            null);
    }

    private async Task<bool> SceneExistsAsync(Guid campaignId, Guid sceneId)
    {
        return await _dbContext.CampaignScenes
            .AsNoTracking()
            .AnyAsync(scene => scene.Id == sceneId && scene.CampaignId == campaignId);
    }

    private async Task<SceneBoard?> GetOrCreateBoardAsync(Guid campaignId, Guid sceneId)
    {
        if (!await SceneExistsAsync(campaignId, sceneId))
        {
            return null;
        }

        var board = await _dbContext.SceneBoards.SingleOrDefaultAsync(currentBoard => currentBoard.SceneId == sceneId);
        if (board is not null)
        {
            return board;
        }

        board = new SceneBoard
        {
            Id = Guid.NewGuid(),
            SceneId = sceneId,
            Version = 0,
            State = SceneBoardState.CreateEmpty(),
            UpdatedAt = DateTimeOffset.UtcNow,
            UpdatedBy = CurrentUserIdOrNull()
        };

        await _dbContext.SceneBoards.AddAsync(board);
        return board;
    }

    private async Task<BoardOperationEnvelope> PersistOperationAsync(
        SceneBoard board,
        string opId,
        string kind,
        object? payload)
    {
        board.Version += 1;
        board.UpdatedAt = DateTimeOffset.UtcNow;
        board.UpdatedBy = CurrentUserIdOrNull();

        await _dbContext.SaveChangesAsync();

        var operation = BuildEnvelope(board.SceneId, board.Version, opId, kind, payload);
        await _sceneNotificationService.NotifyBoardOperation(board.SceneId, operation);
        return operation;
    }

    private Guid? CurrentUserIdOrNull()
    {
        return _currentUser.User.Id == Guid.Empty
            ? null
            : _currentUser.User.Id;
    }

    private static BoardOperationEnvelope BuildEnvelope(
        Guid sceneId,
        int version,
        string opId,
        string kind,
        object? payload)
    {
        return new BoardOperationEnvelope
        {
            SceneId = sceneId,
            Version = version,
            OpId = opId,
            Kind = kind,
            Payload = payload
        };
    }

    private static SceneBoardDocument ToDocument(SceneBoard board)
    {
        return new SceneBoardDocument
        {
            SceneId = board.SceneId,
            Version = board.Version,
            Viewport = CloneViewport(board.State.Viewport),
            Strokes = board.State.Strokes.Select(CloneStroke).ToList(),
            Tokens = board.State.Tokens.Select(CloneToken).ToList(),
            BackgroundUrl = board.State.BackgroundUrl
        };
    }

    private static SceneBoardViewport CloneViewport(SceneBoardViewport viewport)
    {
        return new SceneBoardViewport
        {
            X = viewport.X,
            Y = viewport.Y,
            Scale = viewport.Scale
        };
    }

    private static SceneBoardStroke CloneStroke(SceneBoardStroke stroke)
    {
        return new SceneBoardStroke
        {
            Id = stroke.Id,
            Color = stroke.Color,
            Width = stroke.Width,
            Points = [.. stroke.Points],
            CreatedAt = stroke.CreatedAt,
            CreatedBy = stroke.CreatedBy
        };
    }

    private static SceneBoardToken CloneToken(SceneBoardToken token)
    {
        return new SceneBoardToken
        {
            Id = token.Id,
            Label = token.Label,
            X = token.X,
            Y = token.Y,
            Width = token.Width,
            Height = token.Height,
            Color = token.Color,
            ZIndex = token.ZIndex,
            ImageUrl = token.ImageUrl,
            CreatureId = token.CreatureId,
            Locked = token.Locked
        };
    }
}
