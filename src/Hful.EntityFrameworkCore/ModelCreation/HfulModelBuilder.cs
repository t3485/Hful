using Hful.Domain.Shared;
using Hful.Domain.Shared.ModelCreation;
using Hful.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;

namespace Hful.EntityFrameworkCore.ModelCreation
{
    internal class HfulModelBuilder : IModelBuilder
    {
        private readonly ModelBuilder modelBuilder;

        public HfulModelBuilder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public IEntityBuilder<T> Entity<T>() where T : BaseEntity
        {
            var b = modelBuilder.Entity<T>();
            b.ConfigAutoProperty();
            return new HfulEntityBuilder<T>(b);
        }

        public IModelBuilder Entity<T>(Action<IEntityBuilder<T>> action) where T : BaseEntity
        {
            var b = modelBuilder.Entity<T>();
            b.ConfigAutoProperty();
            action(new HfulEntityBuilder<T>(b));

            return this;
        }
    }
}
