// DrawingHub.cs — ASP.NET Core SignalR Hub
// Pacote: Microsoft.AspNetCore.SignalR (já incluso no .NET 6+)
//
// Program.cs:
//   builder.Services.AddSignalR();
//   app.MapHub<DrawingHub>("/hubs/drawing");

using Microsoft.AspNetCore.SignalR;

namespace MeuApp.Hubs;

public record StrokePoint(double X, double Y);

public record DrawingEvent(
    string UserId,
    IEnumerable<StrokePoint> Points,
    string Color,
    double StrokeWidth);

public record TokenEvent(
    string TokenId,
    string UserId,
    double X,
    double Y);

public class DrawingHub : Hub
{
    private readonly IDrawingRepository _repo;

    public DrawingHub(IDrawingRepository repo)
    {
        _repo = repo;
    }

    // Cliente entra numa sala (grupo SignalR)
    public async Task JoinRoom(string roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

        // Opcional: enviar estado atual do canvas para o novo usuário
        var state = await _repo.GetRoomStateAsync(roomId);
        await Clients.Caller.SendAsync("LoadState", state);
    }

    // Recebe traço de desenho e faz broadcast para o grupo
    public async Task SendStroke(DrawingEvent evt)
    {
        var roomId = Context.GetHttpContext()!.Request.Query["roomId"].ToString();

        // Salva no banco de dados
        await _repo.SaveStrokeAsync(roomId, evt);

        // Publica para os OUTROS usuários da sala (excluindo quem enviou)
        await Clients.OthersInGroup(roomId).SendAsync("ReceiveStroke", evt);
    }

    // Recebe movimentação de token
    public async Task SendTokenMoved(TokenEvent evt)
    {
        var roomId = Context.GetHttpContext()!.Request.Query["roomId"].ToString();

        await _repo.UpdateTokenPositionAsync(roomId, evt);

        await Clients.OthersInGroup(roomId).SendAsync("ReceiveTokenMoved", evt);
    }

    // Recebe token novo adicionado
    public async Task SendTokenAdded(TokenEvent evt)
    {
        var roomId = Context.GetHttpContext()!.Request.Query["roomId"].ToString();

        await _repo.SaveTokenAsync(roomId, evt);

        await Clients.OthersInGroup(roomId).SendAsync("ReceiveTokenAdded", evt);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // SignalR remove automaticamente da Group ao desconectar
        await base.OnDisconnectedAsync(exception);
    }
}

// ─── Interface do repositório (implemente com EF Core / Dapper / etc.) ──────────────────────────

public interface IDrawingRepository
{
    Task SaveStrokeAsync(string roomId, DrawingEvent evt);
    Task SaveTokenAsync(string roomId, TokenEvent evt);
    Task UpdateTokenPositionAsync(string roomId, TokenEvent evt);
    Task<RoomState> GetRoomStateAsync(string roomId);
}

public record RoomState(
    IEnumerable<DrawingEvent> Strokes,
    IEnumerable<TokenEvent> Tokens);
