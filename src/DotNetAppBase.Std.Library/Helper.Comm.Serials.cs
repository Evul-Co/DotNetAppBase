using System.IO.Ports;

namespace DotNetAppBase.Std.Library;

public static partial class XHelper
{
    public static partial class Comm
    {
        public static class Serials
        {
            public static string[] GetPorts() => SerialPort.GetPortNames();
        }
    }
}