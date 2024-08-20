using Microsoft.AspNetCore.SignalR;
using RoleRollsPocketEdition.Application.Campaigns.Dtos;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Core.NotificationUpdate;

namespace RoleRollsPocketEdition.Application.Campaigns.ApplicationServices;

public interface ISceneNotificationService
{
    Task NotifyScene(Guid sceneId, SceneHistoryDto message);
}

public class SceneNotificationService : ISceneNotificationService, ITransientDepency
{
    private readonly IHubContext<SceneHub, ISceneHub> _hubContext;

    public SceneNotificationService(IHubContext<SceneHub, ISceneHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyScene(Guid sceneId, SceneHistoryDto message)
    {
        await _hubContext.Clients.Group(SceneHub.SceneGroup(sceneId)).UpdateHistory(message);
    }
}