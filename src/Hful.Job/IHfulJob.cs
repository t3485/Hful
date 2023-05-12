namespace Hful.Job
{
    public interface IHfulJob
    {
        string Cron { get; }

        Task Execute(HfulJobContext context);
    }
}
