using System.Reflection.Emit;

namespace Hful.Domain.Shared.ModelCreation
{
    public interface IPropertyBuilder<TProperty>
    {
        IPropertyBuilder<TProperty> IsRequired();

        public IPropertyBuilder<TProperty> HasMaxLength(int n);
    }
}
