using System;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public class Emails
    {
        public static void SaveMailMessage(MailMessage msg, string filePath)
        {
            using var fs = new FileStream(filePath, FileMode.Create);
            ExportToEml(msg, fs);
        }

        private static void ExportToEml(MailMessage msg, Stream str)
        {
            using var client = new SmtpClient();
            var path = Path.Combine(Path.GetTempPath(), $"DotNetAppBase.Temp\\{Guid.NewGuid()}\\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            client.UseDefaultCredentials = true;
            client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            client.PickupDirectoryLocation = path;
            client.Send(msg);

            var filePath = Directory.GetFiles(path).Single();
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                fs.CopyTo(str);
            }

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
    }
}