using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Domain.Shared
{
    public class AuditedEntity : BaseEntity
    {
        public DateTime CreatedTime { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime? UpdatedTime { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}
