using Hful.Core.UnitOfWork;

using Microsoft.EntityFrameworkCore.Storage;

namespace Hful.EntityFrameworkCore.UnitOfWork
{
    internal class UowManager : IUowManager
    {
        private readonly HfulContext _context;

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

            var transaction = _context.Database.BeginTransaction();
            return Current = new UnitOfWork(_context, transaction, this);
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
