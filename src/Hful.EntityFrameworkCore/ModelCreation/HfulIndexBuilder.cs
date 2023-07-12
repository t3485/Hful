using Hful.Domain.Shared.ModelCreation;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.EntityFrameworkCore.ModelCreation
{
    internal class HfulIndexBuilder<T> : IIndexBuilder<T>
    {
        private readonly IndexBuilder<T> indexBuilder;

        public HfulIndexBuilder(IndexBuilder<T> indexBuilder)
        {
            this.indexBuilder = indexBuilder;
        }

        public IIndexBuilder<T> IsUnique()
        {
            indexBuilder.IsUnique();
            return this;
        }
    }
}
