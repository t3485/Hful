using Hful.File.Providers;

namespace Hful.File.Configuration
{
    public class AttachmentConfiguration
    {
        public static IAttachmentProvider DefaultProvider { get; set; }

        internal static IAttachmentProvider? GetProvider(string name)
        {
            throw new NotImplementedException();
        }
    }
}
