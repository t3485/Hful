using Hful.Domain.Iam;
using Hful.File.Domain;

using Microsoft.EntityFrameworkCore;

namespace Hful.EntityFrameworkCore.Extensions
{
    public static class AttachmentCreatingModuleExtension
    {
        public static void ConfigureAttachment(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attachment>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("f_attachment");
                b.Property(x => x.Provider).IsRequired().HasMaxLength(16);
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
                b.Property(x => x.Path).IsRequired().HasMaxLength(128);
                b.Property(x => x.FileSize).IsRequired();
                b.Property(x => x.Hash).IsRequired().HasMaxLength(64);
            });

            modelBuilder.Entity<AttachmentRelation>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("f_attachment_relation");
                b.Property(x => x.DisplayName).HasMaxLength(128);
                b.Property(x => x.BusinessType).IsRequired().HasMaxLength(16);
            });
        }
    }
}
