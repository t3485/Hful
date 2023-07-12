using Hful.Domain.Shared.ModelCreation;

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
    }
}
