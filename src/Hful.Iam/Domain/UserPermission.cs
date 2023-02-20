using Hful.Domain.Shared;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Domain.Iam
{
    public class UserPermission : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
