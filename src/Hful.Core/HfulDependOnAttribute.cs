namespace Hful.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class HfulDependOnAttribute : Attribute
    {
        public Type[] DependedTypes { get; }

        public HfulDependOnAttribute(params Type[] dependedTypes)
        {
            DependedTypes = dependedTypes ?? Array.Empty<Type>();
        }

        public virtual Type[] GetDependedTypes()
        {
            return DependedTypes;
        }
    }
}
