using System.Threading;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class Async
    {
        public static class Threads
        {
            public static Thread RunInSta(ThreadStart action)
            {
                var thread = new Thread(action);
                thread.SetApartmentState(ApartmentState.STA);

                thread.Start();

                return thread;
            }
        }
    }
}