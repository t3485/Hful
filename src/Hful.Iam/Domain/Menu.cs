using Hful.Domain.Shared;
using Hful.Iam.Emuns;

namespace Hful.Iam.Domain
{
    public class Menu : AuditedEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? TenantId { get; set; }

        public MenuType Type { get; set; }

        public string? Url { get; set; }

        public string? Component { get; set; }

        public string? Redirect { get; set; }
    }
}
