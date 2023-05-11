using Hful.Core.UnitOfWork;

using Microsoft.EntityFrameworkCore.Storage;

namespace Hful.EntityFrameworkCore.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly HfulContext _context;
        private readonly IUowManager _uowManager;

        private IDbContextTransaction? _transaction;

        public UnitOfWork(HfulContext context, IUowManager uowManager)
        {
            _context = context;
            _uowManager = uowManager;
        }

        public async Task Begin()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public Task CompleteAsync()
        {
            return _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return _transaction.DisposeAsync();
        }

        public Task RollbackAsync()
        {
            return _transaction.RollbackAsync();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
