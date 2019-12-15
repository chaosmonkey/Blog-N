using System;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Blogn
{
    public static class LoggerExtensions
    {
        public static void LogException(this ILogger logger, string message, Exception ex)
        {
            var builder = (!string.IsNullOrWhiteSpace(message))
                ?new StringBuilder(message)
                :new StringBuilder();
            var current = ex;
            var header = "Exception";
            while (current != null)
            {
                builder.AppendLine($"{header}:{current.GetType().FullName}");
                builder.AppendLine(current.Message);
                builder.AppendLine("Stack Trace:");
                builder.AppendLine(current.StackTrace);
                current = current.InnerException;
                header = "Inner Exception";
            }
            logger.LogError(builder.ToString());
        }

        public static void LogException(this ILogger logger, Exception ex)
        {
            LogException(logger, null, ex);
        }
    }
}
