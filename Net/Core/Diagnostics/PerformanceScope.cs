using System;
using System.Globalization;
using Primavera.Platform.CloudServices900.Helpers;

namespace Primavera.Platform.CloudServices900.Diagnostics
{
    /// <summary>
    /// Performance scope class.
    /// </summary>
    public sealed class PerformanceScope : IDisposable
    {
        #region Class Members

        private static int staticCounter = 1;

        #endregion

        #region Members

        private int dynamicCounter;
        private string dynamicSource;

        private DateTime startTime;
        private DateTime endTime;        

        #endregion

        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceScope" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public PerformanceScope(string source)
        {
            this.dynamicCounter = staticCounter++;
            this.dynamicSource = source;

            this.startTime = DateTime.Now;
            LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "{0}\t{1:dd-MM-yyyy hh:mm:ss.fff}\tBEGIN", this.dynamicCounter, this.startTime), this.dynamicSource, LogHelper.LogEventType.Performance);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Executes the delegate function and logs the performance.
        /// </summary>
        /// <typeparam name="T">The function return type.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="function">The function.</param>
        /// <returns>The function type.</returns>
        internal static T Execute<T>(string source, Func<T> function)
        {
            int localCounter = staticCounter++;

            DateTime startTime = DateTime.Now;
            LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "{0}\t{1:dd-MM-yyyy hh:mm:ss.fff}\tBEGIN", localCounter, startTime), source, LogHelper.LogEventType.Performance);

            T result = function();

            DateTime endTime = DateTime.Now;
            LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "{0}\t{1:dd-MM-yyyy hh:mm:ss.fff\tEND}", localCounter, endTime), source, LogHelper.LogEventType.Performance);

            TimeSpan elapsedTime = endTime - startTime;
            LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "{0}\t{1:00}:{2:00}:{3:00}.{4:00}\tDIFF", localCounter, elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds), source, LogHelper.LogEventType.Performance);

            return result;
        }

        /// <summary>
        /// Executes the delegate function and logs the performance.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="function">The function which does not return values.</param>
        internal static void Execute(string source, Action function)
        {
            int innerCounter = staticCounter++;

            DateTime startTime = DateTime.Now;
            LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "{0}\t{1:dd-MM-yyyy hh:mm:ss.fff}\tBEGIN", innerCounter, startTime), source, LogHelper.LogEventType.Performance);

            function();

            DateTime endTime = DateTime.Now;
            LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "{0}\t{1:dd-MM-yyyy hh:mm:ss.fff\tEND}", innerCounter, endTime), source, LogHelper.LogEventType.Performance);

            TimeSpan elapsedTime = endTime - startTime;
            LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "{0}\t{1:00}:{2:00}:{3:00}.{4:00}\tDIFF", innerCounter, elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds), source, LogHelper.LogEventType.Performance);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            this.endTime = DateTime.Now;
            LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "{0}\t{1:dd-MM-yyyy hh:mm:ss.fff\tEND}", this.dynamicCounter, this.endTime), this.dynamicSource, LogHelper.LogEventType.Performance);

            TimeSpan elapsedTime = this.endTime - this.startTime;
            LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "{0}\t{1:00}:{2:00}:{3:00}.{4:00}\tDIFF", this.dynamicCounter, elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds), this.dynamicSource, LogHelper.LogEventType.Performance);
        }

        #endregion
    }
}
