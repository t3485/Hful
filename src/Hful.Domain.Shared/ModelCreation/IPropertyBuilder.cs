using System.Reflection.Emit;

namespace Hful.Domain.Shared.ModelCreation
{
    public interface IPropertyBuilder<TProperty>
    {
        IPropertyBuilder<TProperty> IsRequired();

        IPropertyBuilder<TProperty> HasMaxLength(int n);

        IPropertyBuilder<TProperty> HasPrecision(int precision);

        IPropertyBuilder<TProperty> HasPrecision(int precision, int scale);

        IPropertyBuilder<TProperty> HasDefaultValue(object? value);

        IPropertyBuilder<TProperty> HasComment(string? comment);
    }
}
