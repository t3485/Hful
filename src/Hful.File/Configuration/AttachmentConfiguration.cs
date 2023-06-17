using Hful.File.Providers;

using System.Diagnostics.CodeAnalysis;

namespace Hful.File.Configuration
{
    public class AttachmentConfiguration
    {
        public static IAttachmentProvider DefaultProvider { get; set; } = new DefaultAttachmentProvider();

        private static Dictionary<string, IAttachmentProvider> providers = new();

        public static void AddProvider([NotNull] IAttachmentProvider provider)
        {
            providers.Add(provider.Name, provider);
        }

        internal static IAttachmentProvider? GetProvider(string name)
        {
            return providers[name];
        }

        static AttachmentConfiguration()
        {
            AddProvider(DefaultProvider);
        }
    }
}
