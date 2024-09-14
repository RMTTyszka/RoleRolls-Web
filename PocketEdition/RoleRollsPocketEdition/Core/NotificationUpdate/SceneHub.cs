using Microsoft.AspNetCore.SignalR;
using RoleRollsPocketEdition._Application.Campaigns.Dtos;

namespace RoleRollsPocketEdition.Core.NotificationUpdate;

public class SceneHub : Hub<ISceneHub>
{
    public static string SceneGroup(Guid sceneId) => $"SceneGroup_{sceneId}";
    public async Task UpdateHistory(string scene, SceneHistory message)
    {
        await Clients.Group(scene).UpdateHistory(message);
    }
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}   

public interface ISceneHub  
{
    Task UpdateHistory(SceneHistory message);
}