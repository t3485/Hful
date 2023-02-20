using Hful.Domain.Shared;

namespace Hful.Domain.Iam
{
    public class User : AuditedEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string DisplayName { get; set; }
    }
}
