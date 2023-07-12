using Hful.Domain.Iam;

namespace Hful.Iam.Api.Dto.Authorization
{
    public class CurrentUserDto
    {
        public Guid? Id { get; set; }

        public string? UserName { get; set; }

        public bool IsSuperAdmin { get; set; }

        public List<Role>? Roles { get; set; }
    }
}
