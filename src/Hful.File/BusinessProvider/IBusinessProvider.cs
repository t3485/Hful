using Hful.File.Dto;

namespace Hful.File.BusinessProvider
{
    public interface IBusinessProvider
    {
        string Type { get; }

        Guid Id { get; }

        Guid? TenantId { get; }

        bool HasPermission(AttachmentDto attachment);
    }
}
