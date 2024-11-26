using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Common.Extensions;

public static class TaskExtensions
{
    public static void SafeFireAndForget(
        this Task task,
        ILogger? logger,
        bool breakOnException = true,
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = -1,
#if DEBUG
        [CallerArgumentExpression(nameof(task))]
#endif
        string callerArgumentExpression = "")
    {
        task.ConfigureAwait(true).SafeFireAndForget(logger, breakOnException, callerMemberName, callerFilePath, callerLineNumber, callerArgumentExpression);
    }

#pragma warning disable VSTHRD100 // Avoid async void methods
    public static async void SafeFireAndForget(
#pragma warning restore VSTHRD100 // Avoid async void methods
        this ConfiguredTaskAwaitable task,
        ILogger? logger,
        bool breakOnException = true,
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = -1,
#if DEBUG
        [CallerArgumentExpression(nameof(task))]
#endif
        string callerArgumentExpression = "")
    {
        try
        {
            await task;
        }
        catch (Exception ex)
        {
            TryLogException(logger, ex, breakOnException, callerMemberName, callerFilePath, callerLineNumber, callerArgumentExpression);
        }
    }

    private static void TryLogException(
    ILogger? logger,
    Exception ex,
    bool breakOnException,
    string callerMemberName,
    string callerFilePath,
    int callerLineNumber,
    string callerArgumentExpression)
    {
        try
        {
            logger?.LogError(
                ex,
                "Exception in task handed over to {SafeFireAndForget} (Thread-ID: {ThreadId}): '{ExceptionMessage}'. Caller: '{CallerMemberName}' in {CallerFilePath}:{CallerLineNumber}\nExpression: {CallerArgumentExpression}",
                nameof(SafeFireAndForget),
                Environment.CurrentManagedThreadId,
                ex.Message,
                callerMemberName,
                callerFilePath,
                callerLineNumber,
                callerArgumentExpression);
        }
        catch (/*ObjectDisposed*/Exception secondaryEx)
        {
            var msg = string.Format(
                "Exception in task handed over to {0} (Thread-ID: {1}): '{2}'. Caller: '{3}' in {4}:{5}\nExpression: {6}\nFirst exception: {7}\nSecondary exception: {8}",
                nameof(SafeFireAndForget),
                Environment.CurrentManagedThreadId,
                secondaryEx.Message,
                callerMemberName,
                callerFilePath,
                callerLineNumber,
                callerArgumentExpression,
                ex,
                secondaryEx);

            Console.WriteLine(msg);
            Debug.WriteLine(msg);
            Debugger.Break();
        }

        if (breakOnException)
        {
            Debugger.Break();
        }
    }
}
