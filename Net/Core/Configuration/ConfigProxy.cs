using System;
using System.Runtime.InteropServices;
using BinaryLeaks.Core.Base;
using BinaryLeaks.Core.Helpers;

namespace BinaryLeaks.Core.Configuration
{
    /// <summary>
    /// Class exposed to COM. 
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(Guids.ConfigProxy)]
    public class ConfigProxy
        : DisposableBase, IConfigProxy
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigProxy"/> class.
        /// </summary>
        public ConfigProxy()
            : base()
        {
        }

        #endregion

        #region IConfigProxy Members

        /// <summary>
        /// Compatibility stub to maintain COM compatibility.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The response.
        /// </returns>
        public object CompatibilityStub(object request)
        {
            // Add new methods here.
            return null;
        }

        /// <summary>
        /// Gets the application path.
        /// </summary>
        /// <value>The application path.</value>
        public string ApplicationPath
        {
            get
            {
                return AssemblyHelper.ApplicationPath;
            }
        }

        /// <summary>
        /// Gets the assembly path.
        /// </summary>
        /// <value>The assembly path.</value>
        public string AssemblyPath
        {
            get
            {
                return AssemblyHelper.AssemblyPath;
            }
        }

        /// <summary>
        /// Gets the application configuration file.
        /// </summary>
        /// <value>The application configuration file.</value>
        public string ApplicationConfigurationFile
        {
            get
            {
                return ConfigHelper.ApplicationConfigurationFile;
            }
        }

        /// <summary>
        /// Gets the assembly configuration file.
        /// </summary>
        /// <value>The assembly configuration file.</value>
        public string AssemblyConfigurationFile
        {
            get
            {
                return ConfigHelper.AssemblyConfigurationFile;
            }
        }

        /// <summary>
        /// Gets the cloud connector routing address.
        /// </summary>
        /// <value>The cloud connector routing address.</value>
        public string CloudConnectorRoutingAddress
        {
            get
            {
                return ConfigSettings.CloudConnectorRoutingAddress;
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Releases unmanaged and managed resources (optional).        
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.

            if (!this.Disposed)
            {
                // Dispose managed resources

                if (disposing)
                {
                    // do nothing
                }

                // Dispose unmanaged resources

                // do nothing
            }

            // Dispose on base class

            base.Dispose(disposing);
        }

        #endregion
    }
}
