using Hful.Core.UnitOfWork;

namespace Hful.EntityFrameworkCore.UnitOfWork
{
    internal class ChildUnitOfWork : IUnitOfWork
    {
        public Task CompleteAsync()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }

        public Task RollbackAsync()
        {
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
