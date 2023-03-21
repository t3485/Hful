using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Iam.Dto
{
    public class MenuDto
    {
        public List<MenuDto> Children { get; set; }

        public Guid? ParentId { get; set; }

        public Guid Id { get; set; }
    }
}
