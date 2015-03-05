using Primavera.Platform.CloudServices900.Configuration;
using Primavera.Platform.CloudServices900.Diagnostics;
using Primavera.Platform.CloudServices900.Helpers;

namespace Primavera.Platform.CloudServices900.Base
{
    /// <summary>
    /// Base class that implements multiple assembly issues like the resolver behavior.
    /// </summary>
    public abstract class AssemblyBase
    {
        #region Static Constructors

        /// <summary>
        /// Initializes static members of the <see cref="AssemblyBase"/> class.
        /// </summary>
        static AssemblyBase()
        {
            // Init the current domain on the static constructor (before any instance constructor)
            // to make it for the very first time

            OnCurrentDomainStartup();
        }

        #endregion

        #region Protected Static Methods

        /// <summary>
        /// Called when [current domain startup].
        /// </summary>
        protected static void OnCurrentDomainStartup()
        {
            // Attach global event handlers of current domain

            AssemblyHelper.AttachCurrentDomainEventHandlers();

            // Trigger the loading of current configuration
            // Runtime injection of assembly.dll.config into application.exe.config
            
            // CA1804 : Microsoft.Performance
            // Justification: Suppressed to force the configuration loading.

            var config = ConfigSettings.CurrentConfiguration;
        }

        /// <summary>
        /// Called when [current domain shutdown].
        /// </summary>
        protected static void OnCurrentDomainShutdown()
        {
            AssemblyHelper.DettachCurrentDomainEventHandlers();
        }

        #endregion
    }
}
