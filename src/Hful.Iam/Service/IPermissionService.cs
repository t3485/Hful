using Hful.Iam.Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Iam.Service
{
    public interface IPermissionService
    {
        Task<List<MenuDto>> GetMenuAsync(Guid? tenantId, Guid userId);

        Task<List<string>> GetPermissionAsync(Guid? tenantId, Guid userId);
    }
}
