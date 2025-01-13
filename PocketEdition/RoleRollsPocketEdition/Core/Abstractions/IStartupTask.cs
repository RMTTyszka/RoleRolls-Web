namespace RoleRollsPocketEdition.Core.Abstractions;

public interface IStartupTask
{
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}