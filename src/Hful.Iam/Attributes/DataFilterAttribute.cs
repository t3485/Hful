using System.Linq.Expressions;

namespace Hful.Iam.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class DataFilterAttribute<T> : Attribute
    {
        public bool OverrideByDatabase { get; set; }

        public Expression<Func<T, bool>>? Condition { get; set; }

        public string Code { get; set; }

        public DataFilterAttribute(string code, bool overrideByDatabase = true)
        {
            Code = code;
            OverrideByDatabase = overrideByDatabase;
        }
    }
}
