using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Domain.Shared.ModelCreation
{
    public interface IIndexBuilder<T>
    {
        IIndexBuilder<T> IsUnique();
    }
}
