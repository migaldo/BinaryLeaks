using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Primavera.Platform.CloudServices900.Helpers
{
    /// <summary>
    /// Log Definitions.
    /// </summary>
    internal static class LogHelper
    {
        #region Private Class Members

        #endregion

        #region Public Enums

        /// <summary>
        /// Log event type enumeration.
        /// </summary>
        public enum LogEventType
        {
            /// <summary>
            /// Default log event.
            /// </summary>
            Default = 0,

            /// <summary>
            /// Verbose log event.
            /// </summary>
            Verbose = 1,

            /// <summary>
            /// Performance log event.
            /// </summary>
            Performance = 2
        }

        #endregion

        #region Internal Class Methods

        /// <summary>
        /// Logs the application event.
        /// </summary>
        /// <param name="message">The message.</param>
        internal static void ApplicationLog(string message)
        {
            string source = GetStackTraceSource(2);
            ApplicationLog(message, source, LogEventType.Verbose);
        }

        /// <summary>
        /// Logs the application event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="source">The source.</param>
        internal static void ApplicationLog(string message, string source)
        {
            ApplicationLog(message, source, LogEventType.Verbose);
        }

        /// <summary>
        /// Logs the application event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="logTarget">The log target.</param>
        internal static void ApplicationLog(string message, LogEventType logTarget)
        {
            string source = GetStackTraceSource(2);
            ApplicationLog(message, source, logTarget);
        }

        /// <summary>
        /// Logs the application event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="source">The source.</param>
        /// <param name="logTarget">The log target.</param>
        internal static void ApplicationLog(string message, string source, LogEventType logTarget)
        {
            ApplicationLog(null, message, source, logTarget);
        }

        /// <summary>
        /// Logs the application event.
        /// </summary>
        /// <param name="logFile">The log file.</param>
        /// <param name="message">The message.</param>
        /// <param name="source">The source.</param>
        /// <param name="logTarget">The log target.</param>
        internal static void ApplicationLog(string logFile, string message, string source, LogEventType logTarget)
        {
            try
            {
                // In order to activate default logging you must create the following files 
                // just before the application startup (they are like log activators):
                // - Create an empty file named "<ApplicationName>.verbose.log" for verbose logging.
                // - Create an empty file named "<ApplicationName>.log" for default logging.

                if (string.IsNullOrEmpty(logFile))
                {
                    switch (logTarget)
                    {
                        case LogEventType.Default:

                            logFile = GetDefaultLogFile();

                            if (File.Exists(logFile))
                            {
                                string logSource = source;
                                string logMessage = string.Format(CultureInfo.InvariantCulture, "{0:dd-MM-yyyy hh:mm:ss.fff}\t{1}\t{2}\r\n", System.DateTime.Now, logSource, message);

                                ApplicationLogFile(logFile, logMessage);
                            }

                            break;

                        case LogEventType.Verbose:

                            logFile = GetDefaultVerboseLogFile();

                            if (File.Exists(logFile))
                            {
                                string logSource = source;
                                string logMessage = string.Format(CultureInfo.InvariantCulture, "{0:dd-MM-yyyy hh:mm:ss.fff}\t{1}\t{2}\r\n", System.DateTime.Now, logSource, message);

                                ApplicationLogFile(logFile, logMessage);
                            }

                            break;

                        case LogEventType.Performance:

                            logFile = GetDefaultPerformanceLogFile();

                            if (File.Exists(logFile))
                            {
                                string logMessage = string.Format(CultureInfo.InvariantCulture, "{0}\t{1}\r\n", message, source);

                                ApplicationLogFile(logFile, logMessage);
                            }

                            break;
                    }
                }
                else
                {
                    string logSource = source;
                    string logMessage = string.Format(CultureInfo.InvariantCulture, "{0:dd-MM-yyyy hh:mm:ss.fff}\t{1}\t{2}\r\n", System.DateTime.Now, logSource, message);

                    ApplicationLogFile(logFile, logMessage);
                }

                ////Primavera.Core.CloudServices.LogHandler.ApplicationLogFile(logFile, message, sourcefull, verbose);
            }
            catch
            {
                // This must be a failsafe method, do nothing on error.
            }
        }

        /// <summary>
        /// Gets the stack trace source.
        /// </summary>
        /// <param name="index">The index of the stack frame.</param>
        /// <returns>The stack frame source.</returns>
        internal static string GetStackTraceSource(int index = 1)
        {
            string source = "Unknown";
            string assemblyName = "Unknown";
            string typeName = "Unknown";
            string methodName = "Unknown";

            // Get source from call stack information

            StackTrace stackTrace = new StackTrace();

            if (stackTrace != null)
            {
                // Display the <index> function call in the stack
                // or, if not available, the calling function call (index = 1)

                int fraIndex = (stackTrace.FrameCount > 1) ? index : 0;

                MethodBase methodBase = stackTrace.GetFrame(fraIndex).GetMethod();

                assemblyName = methodBase.Module.Assembly.GetName().Name;
                typeName = methodBase.ReflectedType.Name;
                methodName = methodBase.Name;
                source = string.Format(CultureInfo.InvariantCulture, "{0}\\{1}\\{2}\\{3}", AssemblyHelper.ApplicationName, assemblyName, typeName, methodName);
            }

            return source;
        }

        #endregion

        #region Private Class Methods

        /// <summary>
        /// Logs the application event.
        /// </summary>
        /// <param name="logFile">The log file.</param>
        /// <param name="logMessage">The log message.</param>
        private static void ApplicationLogFile(string logFile, string logMessage)
        {
            if (!string.IsNullOrEmpty(logFile))
            {
                StreamWriter sw = null;

                try
                {
                    sw = File.AppendText(logFile);
                    sw.WriteLine(logMessage);
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Close();
                    }
                }
            }
        }

        private static string GetDefaultLogFile()
        {
            return System.IO.Path.Combine(AssemblyHelper.ApplicationPath, AssemblyHelper.ApplicationName.Substring(0, AssemblyHelper.ApplicationName.Length - 4) + ".log");
        }

        private static string GetDefaultVerboseLogFile()
        {
            return System.IO.Path.Combine(AssemblyHelper.ApplicationPath, AssemblyHelper.ApplicationName.Substring(0, AssemblyHelper.ApplicationName.Length - 4) + ".verbose.log");
        }

        private static string GetDefaultPerformanceLogFile()
        {
            return System.IO.Path.Combine(AssemblyHelper.ApplicationPath, AssemblyHelper.ApplicationName.Substring(0, AssemblyHelper.ApplicationName.Length - 4) + ".perf.log");
        }

        #endregion
    }
}
