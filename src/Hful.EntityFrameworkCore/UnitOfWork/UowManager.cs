using Hful.Core.UnitOfWork;

using Microsoft.EntityFrameworkCore.Storage;

namespace Hful.EntityFrameworkCore.UnitOfWork
{
    internal class UowManager : IUowManager
    {
        private readonly HfulContext _context;

        private IDbContextTransaction? _transaction;

        public UowManager(HfulContext context)
        {
            _context = context;
        }

        public IUnitOfWork Begin()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                return new ChildUnitOfWork();
            }

            try
            {
                return Current = new UnitOfWork(_context, _transaction, this);
            }
            finally
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }
        }

        public async ValueTask<IUnitOfWork> BeginAsync()
        {
            if (_context.Database.CurrentTransaction != null)
            {

            }

            var _transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                return Current = new UnitOfWork(_context, _transaction, this);
            }
            finally
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        public IUnitOfWork? Current { get; set; }
    }
}
