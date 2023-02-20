using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Iam.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class HfulAuthorizationAttribute : Attribute
    {
        public string[] Permissions { get; set; }

        public HfulAuthorizationAttribute(params string[] permissions)
        {
            Permissions = permissions ?? Array.Empty<string>();
        }
    }
}
