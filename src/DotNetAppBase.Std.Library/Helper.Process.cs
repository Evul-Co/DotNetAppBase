using System;
using System.Diagnostics;
using System.Linq;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static class WinProcess
    {
        private static readonly Lazy<Process> LazyCurrent;

        static WinProcess()
        {
            LazyCurrent = new Lazy<Process>(Process.GetCurrentProcess);
        }

        public static Process Current => LazyCurrent.Value;

        public static string GetAppAssembliesRepository() => AppDomain.CurrentDomain.BaseDirectory;

        public static string GetAppBaseDirectory() => AppDomain.CurrentDomain.BaseDirectory;

        public static int GetProcessID() => Process.GetCurrentProcess().Id;

        public static Process GetRunningInstance()
        {
            return Process
                .GetProcessesByName(Current.ProcessName)
                .FirstOrDefault(t => t.Id != Current.Id);
        }

        public static void KillAllSame()
        {
            Process
                .GetProcessesByName(Current.ProcessName)
                .Where(t => t.Id != Current.Id)
                .ToList()
                .ForEach(t => t.Kill());
        }

        public static void RunWithoutWindows(string fileName, string arguments)
        {
            Process.Start(
                new ProcessStartInfo
                    {
                        FileName = fileName,
                        Arguments = arguments,
                        UseShellExecute = true,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    });
        }
    }
}