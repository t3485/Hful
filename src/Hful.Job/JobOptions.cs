namespace Hful.Job
{
    public class JobOptions
    {
        public List<Type> JobTypes { get; } = new List<Type>();

        public JobOptions AddJob<T>() where T : HfulJob
        {
            JobTypes.Add(typeof(T));
            return this;
        }
    }
}
