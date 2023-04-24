using EFCoreTopics.Database.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCoreTopics.Database.Data.CustomStrategies;

public class LogTransactionsStrategy:IExecutionStrategy
{
    private readonly DbContext _currentDbContext;

    public LogTransactionsStrategy(DbContext currentDbContext)
    {
        _currentDbContext = currentDbContext;
    }

    public TResult Execute<TState, TResult>(TState state, Func<DbContext, TState, TResult> operation, Func<DbContext, TState, ExecutionResult<TResult>>? verifySucceeded)
    {
        var changedEntities = _currentDbContext.ChangeTracker.Entries()
            .Where(t => t.State != EntityState.Unchanged && t.State != EntityState.Detached && t.Entity.GetType() != typeof(DatabaseTransactions))
            .Select(c => new DatabaseTransactions()
            {
                OperationType = c.State.ToString("G"),
                TableName = c.Entity.ToString() ?? "",
                Id = Guid.NewGuid()
            }).ToList();

        var result = operation(_currentDbContext, state);


        if (changedEntities.Any())
        {
            var strategy = _currentDbContext.Database.CreateExecutionStrategy();

            strategy.Execute(() =>
            {
                _currentDbContext.AddRange(changedEntities);
                _currentDbContext.SaveChanges();
            });
        }


        return result;
    }

    public async Task<TResult> ExecuteAsync<TState, TResult>(TState state, Func<DbContext, TState, CancellationToken, Task<TResult>> operation, Func<DbContext, TState, CancellationToken, Task<ExecutionResult<TResult>>>? verifySucceeded,
        CancellationToken cancellationToken = new CancellationToken())
    {

        var changedEntities = _currentDbContext.ChangeTracker.Entries()
            .Where(t => t.State != EntityState.Unchanged && t.State != EntityState.Detached && t.Entity.GetType() !=typeof(DatabaseTransactions))
            .Select(c => new DatabaseTransactions()
            {
                OperationType = c.State.ToString("G"),
                TableName = c.Entity.ToString() ?? "",
                Id = Guid.NewGuid()
            }).ToList();
        var result = await operation(_currentDbContext, state, cancellationToken);


        if (changedEntities.Any())
        {
            var strategy = _currentDbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await _currentDbContext.AddRangeAsync(changedEntities, cancellationToken);
                await _currentDbContext.SaveChangesAsync(cancellationToken);
            });
        }



        return result;
    }

    public bool RetriesOnFailure => false;
}