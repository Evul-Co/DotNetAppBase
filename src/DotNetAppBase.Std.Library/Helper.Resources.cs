using System.IO;
using System.Reflection;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public class Resources
    {
        public static byte[] LoadResource(Assembly asm, string resourceName)
        {
            using var stream = new MemoryStream();
            asm.GetManifestResourceStream($"{asm.GetName().Name}.{resourceName}")?.CopyTo(stream);

            return stream.ToArray();
        }

        public static byte[] LoadResource(object context, string resourceName) => LoadResource(context.GetType().Assembly, resourceName);
    }
}