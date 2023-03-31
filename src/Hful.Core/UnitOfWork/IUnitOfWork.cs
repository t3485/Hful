namespace Hful.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        Task SaveChangesAsync();

        Task CompleteAsync();

        Task RollbackAsync();
    }
}
