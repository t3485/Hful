using Hful.Domain.Shared;

namespace Hful.Domain.Iam
{
    public class Role : AuditedEntity
    {
        public string Code { get;set; }

        public string Name { get;set; }
    }
}
