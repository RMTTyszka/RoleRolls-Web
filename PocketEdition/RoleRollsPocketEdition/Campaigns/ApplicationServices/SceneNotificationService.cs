using Microsoft.AspNetCore.SignalR;
using RoleRollsPocketEdition.Campaigns.Dtos;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.NotificationUpdate;

namespace RoleRollsPocketEdition.Campaigns.ApplicationServices;

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