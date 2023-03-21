using Hful.Domain.Shared;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Iam.Domain
{
    public class Menu : AuditedEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public Guid? TenantId { get; set; }
    }
}
