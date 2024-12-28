namespace RoleRollsPocketEdition._Domain.Global;

public interface IStartupTask
{
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}