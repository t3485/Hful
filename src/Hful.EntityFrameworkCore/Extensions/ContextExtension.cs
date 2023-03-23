using Hful.Domain.Shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hful.EntityFrameworkCore.Extensions
{
    internal static class ContextExtension
    {
        private const int GuidLength = 36;

        public static void ConfigAutoProperty<TEntity>(this EntityTypeBuilder<TEntity> b) where TEntity : BaseEntity
        {
            b.HasKey(x => x.Id);

            if (typeof(AuditedEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ConfigAuditedProperty(b);
            }

            foreach (var item in typeof(TEntity).GetProperties())
            {
                if (item.PropertyType == typeof(Guid))
                    b.Property(item.Name).HasMaxLength(GuidLength).IsRequired();
                else if (item.PropertyType.IsGenericType && item.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && item.PropertyType.GenericTypeArguments[0] == typeof(Guid))
                    b.Property(item.Name).HasMaxLength(GuidLength);
            }
        }

        public static void ConfigAuditedProperty<TEntity>(this EntityTypeBuilder<TEntity> b) where TEntity : class
        {
            b.Property("CreatedTime").IsRequired();
            b.Property("CreatedBy").IsRequired().HasMaxLength(GuidLength);
            b.Property("UpdatedBy").HasMaxLength(GuidLength);
        }
    }
}
