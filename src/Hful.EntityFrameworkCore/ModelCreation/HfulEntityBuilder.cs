using Hful.Domain.Shared;
using Hful.Domain.Shared.ModelCreation;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Hful.EntityFrameworkCore.ModelCreation
{
    internal class HfulEntityBuilder<T> : IEntityBuilder<T> where T : BaseEntity
    {
        private readonly EntityTypeBuilder<T> builder;

        public HfulEntityBuilder(EntityTypeBuilder<T> builder)
        {
            this.builder = builder;
        }

        public IPropertyBuilder<TProperty> Property<TProperty>(Expression<Func<T, TProperty>> value)
        {
            return new HfulPropertyBuilder<TProperty>(builder.Property(value));
        }

        public void ToTable(string v)
        {
            builder.ToTable(v);
        }

        public IIndexBuilder<T> HasIndex(Expression<Func<T, object?>> f)
        {
            return new HfulIndexBuilder<T>(builder.HasIndex(f));
        }
    }
}
