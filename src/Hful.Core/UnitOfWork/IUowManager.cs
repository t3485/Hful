namespace Hful.Core.UnitOfWork
{
    public interface IUowManager
    {
        IUnitOfWork Begin();
    }
}
