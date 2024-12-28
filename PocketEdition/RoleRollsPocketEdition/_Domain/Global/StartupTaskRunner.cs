namespace RoleRollsPocketEdition._Domain.Global;

public class StartupTaskRunner
{
    private readonly IEnumerable<IStartupTask> _startupTasks;

    public StartupTaskRunner(IEnumerable<IStartupTask> startupTasks)
    {
        _startupTasks = startupTasks;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        foreach (var task in _startupTasks)
        {
            await task.ExecuteAsync(cancellationToken);
        }
    }
}