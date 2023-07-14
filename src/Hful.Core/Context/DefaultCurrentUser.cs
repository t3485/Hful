namespace Hful.Core.Context
{
    internal class DefaultCurrentUser : ICurrentUser
    {
        public Guid? Id => null;

        public string? UserName => null;

        public bool IsSuperAdmin => false;

        public List<Guid> RoleIds => new List<Guid>();
    }
}
