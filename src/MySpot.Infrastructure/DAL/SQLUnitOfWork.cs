namespace MySpot.Infrastructure.DAL;

internal sealed class SQLUnitOfWork(MySpotDbContext dbContext) : IUnitOfWork
{
    private readonly MySpotDbContext _dbContext = dbContext;

    public async Task Execute(Func<Task> action)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await action();
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}