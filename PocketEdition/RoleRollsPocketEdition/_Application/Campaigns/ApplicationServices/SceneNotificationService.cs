using Microsoft.AspNetCore.SignalR;
using RoleRollsPocketEdition._Application.Campaigns.Dtos;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Core.NotificationUpdate;

namespace RoleRollsPocketEdition._Application.Campaigns.ApplicationServices;

public interface ISceneNotificationService
{
    Task NotifyScene(Guid sceneId, SceneHistory message);
}

public class SceneNotificationService : ISceneNotificationService, ITransientDependency
{
    private readonly IHubContext<SceneHub, ISceneHub> _hubContext;

    public SceneNotificationService(IHubContext<SceneHub, ISceneHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyScene(Guid sceneId, SceneHistory message)
    {
        await _hubContext.Clients.Group(SceneHub.SceneGroup(sceneId)).UpdateHistory(message);
    }
}