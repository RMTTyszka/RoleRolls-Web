using Microsoft.EntityFrameworkCore;
using Npgsql;
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
    Task<BoardOperationEnvelope?> MoveTokenAsync(Guid campaignId, Guid sceneId, Guid tokenId, BoardCommand<BoardTokenMoveInput> command);
    Task<BoardOperationEnvelope?> RenameTokenAsync(Guid campaignId, Guid sceneId, Guid tokenId, BoardCommand<BoardTokenRenameInput> command);
    Task<BoardOperationEnvelope?> SetTokenLockedAsync(Guid campaignId, Guid sceneId, Guid tokenId, BoardCommand<BoardTokenLockInput> command);
    Task<BoardOperationEnvelope?> RemoveTokenAsync(Guid campaignId, Guid sceneId, Guid tokenId, BoardDeleteCommand command);
    Task<BoardOperationEnvelope?> ClearAsync(Guid campaignId, Guid sceneId, BoardDeleteCommand command);
}

public class SceneBoardService : ISceneBoardService, ITransientDependency
{
    private const int MaxPersistenceRetryAttempts = 5;
    private const string SceneBoardSceneIdIndexName = "IX_SceneBoards_SceneId";

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
        if (!await SceneExistsAsync(campaignId, sceneId))
        {
            return null;
        }

        var stroke = CloneStroke(command.Payload);
        return await ExecuteMutationWithRetryAsync(
            sceneId,
            command.OpId,
            SceneBoardOperationKinds.StrokeAdded,
            createIfMissing: true,
            board =>
            {
                if (board is null)
                {
                    return MutationExecutionResult.Complete(null);
                }

                board.State.Strokes = board.State.Strokes
                    .Where(existingStroke => existingStroke.Id != stroke.Id)
                    .Append(CloneStroke(stroke))
                    .ToList();

                return MutationExecutionResult.Persist(CloneStroke(stroke));
            });
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

        return await ExecuteMutationWithRetryAsync(
            sceneId,
            command.OpId,
            SceneBoardOperationKinds.StrokeRemoved,
            createIfMissing: false,
            board =>
            {
                var payload = new BoardStrokeRemovedPayload { StrokeId = strokeId };
                if (board is null)
                {
                    return MutationExecutionResult.Complete(BuildEnvelope(
                        sceneId,
                        0,
                        command.OpId,
                        SceneBoardOperationKinds.StrokeRemoved,
                        payload));
                }

                var removed = board.State.Strokes.RemoveAll(stroke => stroke.Id == strokeId) > 0;
                if (!removed)
                {
                    return MutationExecutionResult.Complete(BuildEnvelope(
                        sceneId,
                        board.Version,
                        command.OpId,
                        SceneBoardOperationKinds.StrokeRemoved,
                        payload));
                }

                return MutationExecutionResult.Persist(payload);
            });
    }

    public async Task<BoardOperationEnvelope?> UpsertTokenAsync(
        Guid campaignId,
        Guid sceneId,
        Guid tokenId,
        BoardCommand<SceneBoardToken> command)
    {
        if (!await SceneExistsAsync(campaignId, sceneId))
        {
            return null;
        }

        var token = CloneToken(command.Payload);
        token.Id = tokenId;
        return await ExecuteMutationWithRetryAsync(
            sceneId,
            command.OpId,
            SceneBoardOperationKinds.TokenUpserted,
            createIfMissing: true,
            board =>
            {
                if (board is null)
                {
                    return MutationExecutionResult.Complete(null);
                }

                var existingIndex = board.State.Tokens.FindIndex(currentToken => currentToken.Id == token.Id);
                if (existingIndex >= 0)
                {
                    board.State.Tokens[existingIndex] = CloneToken(token);
                }
                else
                {
                    board.State.Tokens.Add(CloneToken(token));
                }

                board.State.Tokens = board.State.Tokens
                    .OrderBy(currentToken => currentToken.ZIndex)
                    .ToList();

                return MutationExecutionResult.Persist(CloneToken(token));
            });
    }

    public async Task<BoardOperationEnvelope?> MoveTokenAsync(
        Guid campaignId,
        Guid sceneId,
        Guid tokenId,
        BoardCommand<BoardTokenMoveInput> command)
    {
        if (!await SceneExistsAsync(campaignId, sceneId))
        {
            return null;
        }

        var payload = new BoardTokenMovedPayload
        {
            TokenId = tokenId,
            X = command.Payload.X,
            Y = command.Payload.Y
        };

        return await ExecuteMutationWithRetryAsync(
            sceneId,
            command.OpId,
            SceneBoardOperationKinds.TokenMoved,
            createIfMissing: false,
            board =>
            {
                if (board is null)
                {
                    return MutationExecutionResult.Complete(BuildEnvelope(
                        sceneId,
                        0,
                        command.OpId,
                        SceneBoardOperationKinds.TokenMoved,
                        payload));
                }

                var token = board.State.Tokens.SingleOrDefault(currentToken => currentToken.Id == tokenId);
                if (token is null)
                {
                    return MutationExecutionResult.Complete(BuildEnvelope(
                        sceneId,
                        board.Version,
                        command.OpId,
                        SceneBoardOperationKinds.TokenMoved,
                        payload));
                }

                if (token.X == payload.X && token.Y == payload.Y)
                {
                    return MutationExecutionResult.Complete(BuildEnvelope(
                        sceneId,
                        board.Version,
                        command.OpId,
                        SceneBoardOperationKinds.TokenMoved,
                        payload));
                }

                token.X = payload.X;
                token.Y = payload.Y;

                return MutationExecutionResult.Persist(payload);
            });
    }

    public async Task<BoardOperationEnvelope?> RenameTokenAsync(
        Guid campaignId,
        Guid sceneId,
        Guid tokenId,
        BoardCommand<BoardTokenRenameInput> command)
    {
        if (!await SceneExistsAsync(campaignId, sceneId))
        {
            return null;
        }

        var payload = new BoardTokenRenamedPayload
        {
            TokenId = tokenId,
            Label = command.Payload.Label
        };

        return await ExecuteMutationWithRetryAsync(
            sceneId,
            command.OpId,
            SceneBoardOperationKinds.TokenRenamed,
            createIfMissing: false,
            board =>
            {
                if (board is null)
                {
                    return MutationExecutionResult.Complete(BuildEnvelope(
                        sceneId,
                        0,
                        command.OpId,
                        SceneBoardOperationKinds.TokenRenamed,
                        payload));
                }

                var token = board.State.Tokens.SingleOrDefault(currentToken => currentToken.Id == tokenId);
                if (token is null)
                {
                    return MutationExecutionResult.Complete(BuildEnvelope(
                        sceneId,
                        board.Version,
                        command.OpId,
                        SceneBoardOperationKinds.TokenRenamed,
                        payload));
                }

                if (token.Label == payload.Label)
                {
                    return MutationExecutionResult.Complete(BuildEnvelope(
                        sceneId,
                        board.Version,
                        command.OpId,
                        SceneBoardOperationKinds.TokenRenamed,
                        payload));
                }

                token.Label = payload.Label;

                return MutationExecutionResult.Persist(payload);
            });
    }

    public async Task<BoardOperationEnvelope?> SetTokenLockedAsync(
        Guid campaignId,
        Guid sceneId,
        Guid tokenId,
        BoardCommand<BoardTokenLockInput> command)
    {
        if (!await SceneExistsAsync(campaignId, sceneId))
        {
            return null;
        }

        var payload = new BoardTokenLockChangedPayload
        {
            TokenId = tokenId,
            Locked = command.Payload.Locked
        };

        return await ExecuteMutationWithRetryAsync(
            sceneId,
            command.OpId,
            SceneBoardOperationKinds.TokenLockChanged,
            createIfMissing: false,
            board =>
            {
                if (board is null)
                {
                    return MutationExecutionResult.Complete(BuildEnvelope(
                        sceneId,
                        0,
                        command.OpId,
                        SceneBoardOperationKinds.TokenLockChanged,
                        payload));
                }

                var token = board.State.Tokens.SingleOrDefault(currentToken => currentToken.Id == tokenId);
                if (token is null)
                {
                    return MutationExecutionResult.Complete(BuildEnvelope(
                        sceneId,
                        board.Version,
                        command.OpId,
                        SceneBoardOperationKinds.TokenLockChanged,
                        payload));
                }

                if (token.Locked == payload.Locked)
                {
                    return MutationExecutionResult.Complete(BuildEnvelope(
                        sceneId,
                        board.Version,
                        command.OpId,
                        SceneBoardOperationKinds.TokenLockChanged,
                        payload));
                }

                token.Locked = payload.Locked;

                return MutationExecutionResult.Persist(payload);
            });
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

        return await ExecuteMutationWithRetryAsync(
            sceneId,
            command.OpId,
            SceneBoardOperationKinds.TokenRemoved,
            createIfMissing: false,
            board =>
            {
                var payload = new BoardTokenRemovedPayload { TokenId = tokenId };
                if (board is null)
                {
                    return MutationExecutionResult.Complete(BuildEnvelope(
                        sceneId,
                        0,
                        command.OpId,
                        SceneBoardOperationKinds.TokenRemoved,
                        payload));
                }

                var removed = board.State.Tokens.RemoveAll(token => token.Id == tokenId) > 0;
                if (!removed)
                {
                    return MutationExecutionResult.Complete(BuildEnvelope(
                        sceneId,
                        board.Version,
                        command.OpId,
                        SceneBoardOperationKinds.TokenRemoved,
                        payload));
                }

                return MutationExecutionResult.Persist(payload);
            });
    }

    public async Task<BoardOperationEnvelope?> ClearAsync(Guid campaignId, Guid sceneId, BoardDeleteCommand command)
    {
        if (!await SceneExistsAsync(campaignId, sceneId))
        {
            return null;
        }

        return await ExecuteMutationWithRetryAsync(
            sceneId,
            command.OpId,
            SceneBoardOperationKinds.BoardCleared,
            createIfMissing: false,
            board =>
            {
                if (board is null)
                {
                    return MutationExecutionResult.Complete(
                        BuildEnvelope(sceneId, 0, command.OpId, SceneBoardOperationKinds.BoardCleared, null));
                }

                if (board.State.Strokes.Count == 0 && board.State.Tokens.Count == 0)
                {
                    return MutationExecutionResult.Complete(
                        BuildEnvelope(sceneId, board.Version, command.OpId, SceneBoardOperationKinds.BoardCleared, null));
                }

                board.State.Strokes = [];
                board.State.Tokens = [];

                return MutationExecutionResult.Persist(null);
            });
    }

    private async Task<bool> SceneExistsAsync(Guid campaignId, Guid sceneId)
    {
        return await _dbContext.CampaignScenes
            .AsNoTracking()
            .AnyAsync(scene => scene.Id == sceneId && scene.CampaignId == campaignId);
    }

    private async Task<SceneBoard?> GetOrCreateBoardAsync(Guid sceneId)
    {
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

    private async Task<BoardOperationEnvelope?> ExecuteMutationWithRetryAsync(
        Guid sceneId,
        string opId,
        string kind,
        bool createIfMissing,
        Func<SceneBoard?, MutationExecutionResult> mutateBoard)
    {
        Exception? lastException = null;

        for (var attempt = 0; attempt < MaxPersistenceRetryAttempts; attempt++)
        {
            try
            {
                _dbContext.ChangeTracker.Clear();

                var board = createIfMissing
                    ? await GetOrCreateBoardAsync(sceneId)
                    : await _dbContext.SceneBoards.SingleOrDefaultAsync(currentBoard => currentBoard.SceneId == sceneId);

                var mutationResult = mutateBoard(board);
                if (mutationResult.CompletedOperation is not null)
                {
                    return mutationResult.CompletedOperation;
                }

                if (board is null)
                {
                    return null;
                }

                if (!mutationResult.ShouldPersist)
                {
                    return BuildEnvelope(sceneId, board.Version, opId, kind, mutationResult.Payload);
                }

                return await PersistOperationAsync(board, opId, kind, mutationResult.Payload);
            }
            catch (DbUpdateConcurrencyException exception) when (attempt < MaxPersistenceRetryAttempts - 1)
            {
                lastException = exception;
            }
            catch (DbUpdateException exception) when (attempt < MaxPersistenceRetryAttempts - 1 && IsSceneBoardCreateRace(exception))
            {
                lastException = exception;
            }
        }

        if (lastException is not null)
        {
            throw lastException;
        }

        return null;
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

    private static bool IsSceneBoardCreateRace(DbUpdateException exception)
    {
        return exception.InnerException is PostgresException
        {
            SqlState: PostgresErrorCodes.UniqueViolation,
            ConstraintName: SceneBoardSceneIdIndexName
        };
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

    private sealed record MutationExecutionResult(
        bool ShouldPersist,
        object? Payload,
        BoardOperationEnvelope? CompletedOperation = null)
    {
        public static MutationExecutionResult Persist(object? payload) => new(true, payload);

        public static MutationExecutionResult Complete(BoardOperationEnvelope? operation) =>
            new(false, null, operation);
    }
}
