using Hful.Core.UnitOfWork;

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
            return Current = new UnitOfWork(_context, this);
        }

        public IUnitOfWork? Current { get; set; }
    }
}
