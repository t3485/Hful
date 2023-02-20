using Hful.Domain.Shared;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.EntityFrameworkCore.Extensions
{
    internal static class ContextExtension
    {
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
                    b.Property(item.Name).HasMaxLength(32).IsRequired();
                else if (item.PropertyType.IsGenericType && item.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && item.PropertyType.GenericTypeArguments[0] == typeof(Guid))
                    b.Property(item.Name).HasMaxLength(32);
            }
        }

        public static void ConfigAuditedProperty<TEntity>(this EntityTypeBuilder<TEntity> b) where TEntity : class
        {
            b.Property("CreatedTime").IsRequired();
            b.Property("CreatedBy").IsRequired().HasMaxLength(32);
            b.Property("UpdatedBy").HasMaxLength(32);
        }
    }
}
