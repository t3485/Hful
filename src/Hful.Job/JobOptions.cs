namespace Hful.Job
{
    public class JobOptions
    {
        public List<Type> JobTypes { get; set; } = new List<Type>();

        public JobOptions AddJob<T>()
        {
            JobTypes.Add(typeof(T));
            return this;
        }
    }
}
