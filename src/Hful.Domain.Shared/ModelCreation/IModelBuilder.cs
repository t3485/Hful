using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Domain.Shared.ModelCreation
{
    public interface IModelBuilder
    {
        IEntityBuilder<T> Entity<T>() where T : BaseEntity;

        IModelBuilder Entity<T>(Action<IEntityBuilder<T>> action) where T : BaseEntity;
    }
}
