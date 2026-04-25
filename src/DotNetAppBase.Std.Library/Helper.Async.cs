using System;
using System.Diagnostics;
using System.Threading;
using DotNetAppBase.Std.Exceptions.Assert;
using Timer = System.Timers.Timer;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class Async
    {
        public static void AsyncWithDelay(Action doWork, Action completeWork, TimeSpan? minSpan = null)
        {
            doWork ??= delegate { };
            completeWork ??= delegate { };

            BeginInvoke(
                () =>
                    {
                        Invoke(doWork, null, minSpan ?? TimeSpan.FromSeconds(0.86));

                        completeWork();
                    });
        }

        public static void AsyncWithDelay<T>(Func<T> doWork, Action<T> completeWork, TimeSpan? minSpan = null)
        {
            doWork ??= () => default;
            completeWork ??= delegate { };

            BeginInvoke(
                () =>
                    {
                        var result = Invoke<T>(doWork, null, minSpan ?? TimeSpan.FromSeconds(0.86));

                        completeWork(result);
                    });
        }

        public static IAsyncResult BeginInvoke(Action method, TimeSpan? delay = null, TimeSpan? minSpan = null) => BeginInvoke((Delegate) method, delay, minSpan);

        public static IAsyncResult BeginInvoke(Delegate method, TimeSpan? delay, TimeSpan? minSpan, params object[] args)
        {
            delay ??= TimeSpan.Zero;
            minSpan ??= TimeSpan.Zero;

            Action action =
                () =>
                    {
                        var watch = new Stopwatch();
                        watch.Start();
                        Thread.Sleep(delay.Value);
                        try
                        {
                            method.DynamicInvoke(args);
                        }
                        catch (Exception ex)
                        {
                            XDebug.OnException(ex);

                            throw;
                        }

                        var elapsed = minSpan.Value - watch.Elapsed;
                        watch.Stop();
                        if (elapsed.TotalSeconds > 0)
                        {
                            Thread.Sleep(elapsed);
                        }
                    };

            return action.BeginInvoke(action.EndInvoke, null);
        }

        public static void Invoke(Action method, TimeSpan? delay = null, TimeSpan? minSpan = null)
        {
            Invoke((Delegate) method, delay, minSpan);
        }

        public static void Invoke(Delegate method, TimeSpan? delay, TimeSpan? minSpan, params object[] args)
        {
            delay ??= TimeSpan.Zero;
            minSpan ??= TimeSpan.Zero;

            void Execute()
            {
                var watch = new Stopwatch();
                watch.Start();
                Thread.Sleep(delay.Value);
                try
                {
                    method.DynamicInvoke(args);
                }
                finally
                {
                    watch.Stop();
                }

                var elapsed = minSpan.Value - watch.Elapsed;

                if (elapsed.TotalSeconds > 0)
                {
                    Thread.Sleep(elapsed);
                }
            }

            Execute();
        }

        public static T Invoke<T>(Delegate method, TimeSpan? delay, TimeSpan? minSpan, params object[] args)
        {
            delay ??= TimeSpan.Zero;
            minSpan ??= TimeSpan.Zero;

            T Execute()
            {
                T result;

                var watch = new Stopwatch();
                watch.Start();
                Thread.Sleep(delay.Value);
                try
                {
                    result = (T) method.DynamicInvoke(args);
                }
                finally
                {
                    watch.Stop();
                }

                var elapsed = minSpan.Value - watch.Elapsed;

                if (elapsed.TotalSeconds > 0)
                {
                    Thread.Sleep(elapsed);
                }

                return result;
            }

            return Execute();
        }

        public static void SetTimeOut(Action method, TimeSpan wait)
        {
            SetTimeOut((Delegate) method, wait);
        }

        public static void SetTimeOut(Delegate method, TimeSpan wait, params object[] args)
        {
            var timer = new Timer {Interval = wait.TotalMilliseconds, AutoReset = false};
            timer.Elapsed
                += (sender, eventArgs) =>
                    {
                        method.DynamicInvoke(args);
                        timer.Dispose();
                    };

            timer.Enabled = true;
        }
    }
}