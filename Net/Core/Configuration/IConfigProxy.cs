using System;
using System.Runtime.InteropServices;

namespace Primavera.Platform.CloudServices900.Configuration
{
    /// <summary>
    /// Interface exposed to COM.
    /// </summary>
    [ComVisible(true)]
    [Guid(Guids.IConfigProxy)]
    public interface IConfigProxy
    {
        /// <summary>
        /// Compatibility stub to maintain COM compatibility.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        object CompatibilityStub(object request);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Gets the application path.
        /// </summary>
        /// <value>The application path.</value>
        string ApplicationPath { get; }

        /// <summary>
        /// Gets the assembly path.
        /// </summary>
        /// <value>The assembly path.</value>
        string AssemblyPath { get; }

        /// <summary>
        /// Gets the application configuration file.
        /// </summary>
        /// <value>The application configuration file.</value>
        string ApplicationConfigurationFile { get; }

        /// <summary>
        /// Gets the assembly configuration file.
        /// </summary>
        /// <value>The assembly configuration file.</value>
        string AssemblyConfigurationFile { get; }

        /// <summary>
        /// Gets the cloud connector routing address.
        /// </summary>
        /// <value>The cloud connector routing address.</value>
        string CloudConnectorRoutingAddress { get; }
    }
}
