using Microsoft.AspNetCore.SignalR;

namespace RoleRollsPocketEdition.Core.NotificationUpdate;

public class SceneHub : Hub<ISceneHub>
{
    public static string SceneGroup(Guid sceneId) => $"SceneGroup_{sceneId}";
    public async Task UpdateHistory(string scene, string text)
    {
        await Clients.Group(scene).UpdateHistory(text);
    }
}   

public interface ISceneHub  
{
    Task UpdateHistory(string text);
}