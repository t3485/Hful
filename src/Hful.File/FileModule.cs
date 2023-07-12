using Hful.Core;
using Hful.File.Domain;
using Hful.File.Service;
using Hful.Domain.Shared.ModelCreation;

using Microsoft.Extensions.DependencyInjection;

namespace Hful.File
{
    [HfulDependOn(typeof(CoreModule))]
    public class FileModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
            context.Services.AddTransient<IAttachmentService, AttachmentService>();

            context.Services.AddDomain(mb =>
            {
                mb.Entity<Attachment>(b =>
                {
                    b.ToTable("f_attachment");
                    b.Property(x => x.Provider).IsRequired().HasMaxLength(16);
                    b.Property(x => x.Name).IsRequired().HasMaxLength(128);
                    b.Property(x => x.Path).IsRequired().HasMaxLength(128);
                    b.Property(x => x.FileSize).IsRequired();
                    b.Property(x => x.Hash).IsRequired().HasMaxLength(64);
                });

                mb.Entity<AttachmentRelation>(b =>
                {
                    b.ToTable("f_attachment_relation");
                    b.Property(x => x.DisplayName).HasMaxLength(128);
                    b.Property(x => x.BusinessType).IsRequired().HasMaxLength(16);
                });

                mb.Entity<AttachmentUpload>(b =>
                {
                    b.ToTable("f_attachment_upload");
                });
            });
        }
    }
}
