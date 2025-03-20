using Microsoft.EntityFrameworkCore.Storage;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Core.EntityFramework;

public interface IUnitOfWork : IDisposable
{
    IUnitOfWork Begin();
    Task Commit();
    Task Rollback();
}

public class UnitOfWork : IUnitOfWork, IScopedDepedency
{
    private readonly RoleRollsDbContext _context;
    private IDbContextTransaction _transaction;
    private bool _transactionCompleted;

    public UnitOfWork(RoleRollsDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork Begin()
    {
        _transaction = _context.Database.BeginTransaction();
        _transactionCompleted = false;
        return this;
    }

    public async Task Commit()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction?.CommitAsync()!;
            _transactionCompleted = true;
        }
        catch
        {
            await _transaction?.RollbackAsync()!;
            throw;
        }
    }

    public async Task Rollback()
    {
        if (!_transactionCompleted)
        {
            await _transaction?.RollbackAsync()!;
            _transactionCompleted = true;
        }
    }
    public void Dispose()
    {
        if (!_transactionCompleted)
        {
            // Se o commit não foi realizado, faz rollback.
            _transaction?.Rollback();
        }

        _transaction?.Dispose();
        _context.Dispose();
    }
}