using Microsoft.EntityFrameworkCore.Storage;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Core.EntityFramework;

public interface IUnitOfWork : IDisposable
{
    IUnitOfWork Begin();
    Task CommitAsync();
    Task RollbackAsync();
}

public class UnitOfWork : IUnitOfWork, IScopedDependency, IAsyncDisposable, IDisposable
{
    private readonly RoleRollsDbContext _context;
    private IDbContextTransaction _transaction;
    private bool _transactionCompleted;

    public UnitOfWork(RoleRollsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork Begin()
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _transaction = _context.Database.BeginTransaction();
        _transactionCompleted = false;
        return this;
    }

    public async Task CommitAsync()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction has been started.");
        }

        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
            _transactionCompleted = true;
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null && !_transactionCompleted)
        {
            await _transaction.RollbackAsync();
            _transactionCompleted = true;
        }
    }

    private async Task DisposeTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        DisposeAsync().GetAwaiter().GetResult();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (!_transactionCompleted && _transaction != null)
        {
            await RollbackAsync();
        }

        await DisposeTransactionAsync();
    }
}
