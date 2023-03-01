using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Domain
{
    public interface ITenant
    {
        Guid? TenantId { get; set; }
    }
}
