namespace Hful.Core.Context
{
    public interface ICurrentUser
    {
        Guid? Id { get; }

        string? UserName { get; }

        bool IsSuperAdmin { get; }

        List<Guid> RoleIds { get; }
    }
}
