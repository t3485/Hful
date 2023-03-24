using Hful.Domain.Shared;

namespace Hful.Iam.Domain
{
    public class Tenant : AuditedEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
