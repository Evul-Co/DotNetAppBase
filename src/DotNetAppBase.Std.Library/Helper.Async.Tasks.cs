using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNetAppBase.Std.Exceptions.Assert;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class Async
    {
        [Localizable(false)]
        public static class Tasks
        {
            public static string CreateTheadName(object context, string alias)
            {
                XContract.ArgIsNotNull(context, nameof(context));
                XContract.ArgIsNotNull(alias, nameof(alias));

                return $"#Task::{context.GetType().Name}->{alias}";
            }

            public static async Task ForeachAsync<T>(IEnumerable<T> source, int maxParallelCount, Func<T, Task> action)
            {
                source = source.ToArray();

                using var completeSemaphoreSlim = new SemaphoreSlim(1);
                using var taskCountLimitSemaphoreSlim = new SemaphoreSlim(maxParallelCount);

                await completeSemaphoreSlim.WaitAsync();
                var runningTaskCount = source.Count();

                foreach (var item in source)
                {
                    await taskCountLimitSemaphoreSlim.WaitAsync();

#pragma warning disable 4014
                    Task.Run(async () =>
#pragma warning restore 4014
                        {
                            try
                            {
                                await action(item).ContinueWith(
                                    task =>
                                        {
                                            Interlocked.Decrement(ref runningTaskCount);
                                            if (runningTaskCount == 0)
                                            {
                                                completeSemaphoreSlim.Release();
                                            }
                                        });
                            }
                            finally
                            {
                                taskCountLimitSemaphoreSlim.Release();
                            }
                        });
                }

                await completeSemaphoreSlim.WaitAsync();
            }

            public static Task Run(object context, string alias, Action actionToRun)
            {
                XContract.ArgIsNotNull(context, nameof(context));
                XContract.ArgIsNotNull(alias, nameof(alias));
                XContract.ArgIsNotNull(actionToRun, nameof(actionToRun));

                return Task.Run(
                    () =>
                        {
                            Thread.CurrentThread.Name = CreateTheadName(context, alias);

                            try
                            {
                                actionToRun();
                            }
                            catch (Exception ex)
                            {
                                XDebug.OnException(ex);

                                throw;
                            }
                        });
            }

            public static Task<T> Run<T>(object context, string alias, Func<T> actionToRun)
            {
                XContract.ArgIsNotNull(context, nameof(context));
                XContract.ArgIsNotNull(alias, nameof(alias));
                XContract.ArgIsNotNull(actionToRun, nameof(actionToRun));

                return Task.Run(
                    () =>
                        {
                            Thread.CurrentThread.Name = CreateTheadName(context, alias);

                            try
                            {
                                return actionToRun();
                            }
                            catch (Exception ex)
                            {
                                XDebug.OnException(ex);

                                throw;
                            }
                        });
            }

            public static async Task Wait(TimeSpan? wait = null)
            {
                await Task.Delay(wait ?? TimeSpan.FromSeconds(2));
            }
        }
    }
}