using System.Linq.Expressions;

namespace Hful.Domain.Shared.ModelCreation
{
    public interface IEntityBuilder<T> where T : BaseEntity
    {
        IPropertyBuilder<TProperty> Property<TProperty>(Expression<Func<T, TProperty>> value);

        void ToTable(string v);

        IIndexBuilder<T> HasIndex(Expression<Func<T, object?>> f);
    }
}
