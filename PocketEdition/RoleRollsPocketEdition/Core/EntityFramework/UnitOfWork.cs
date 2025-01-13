using Microsoft.EntityFrameworkCore.Storage;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Core.EntityFramework;

public interface IUnitOfWork : IDisposable
{
    IUnitOfWork Begin();
    void Commit();
    void Rollback();
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

    public void Commit()
    {
        try
        {
            _context.SaveChanges();
            _transaction?.Commit();
            _transactionCompleted = true;
        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
    }

    public void Rollback()
    {
        if (!_transactionCompleted)
        {
            _transaction?.Rollback();
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