using Hful.Domain.Shared.ModelCreation;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hful.EntityFrameworkCore.ModelCreation
{
    internal class HfulPropertyBuilder<TProperty> : IPropertyBuilder<TProperty>
    {
        private readonly PropertyBuilder<TProperty> propertyBuilder;

        public HfulPropertyBuilder(PropertyBuilder<TProperty> propertyBuilder)
        {
            this.propertyBuilder = propertyBuilder;
        }

        public IPropertyBuilder<TProperty> IsRequired()
        {
            propertyBuilder.IsRequired();
            return this;
        }

        public IPropertyBuilder<TProperty> HasMaxLength(int n)
        {
            propertyBuilder.HasMaxLength(n);
            return this;
        }

        public IPropertyBuilder<TProperty> HasPrecision(int precision)
        {
            propertyBuilder.HasPrecision(precision);
            return this;
        }

        public IPropertyBuilder<TProperty> HasPrecision(int precision, int scale)
        {
            propertyBuilder.HasPrecision(precision, scale);
            return this;
        }

        public IPropertyBuilder<TProperty> HasDefaultValue(object? value)
        {
            propertyBuilder.HasDefaultValue(value);
            return this;
        }

        public IPropertyBuilder<TProperty> HasComment(string? comment)
        {
            propertyBuilder.HasComment(comment);
            return this;
        }
    }
}
