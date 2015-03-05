using System;
using System.Globalization;
using System.IO;
using Primavera.Core.Configuration;
using BinaryLeaks.Core.Diagnostics;
using BinaryLeaks.Core.Helpers;

namespace BinaryLeaks.Core.Configuration
{
    /// <summary>
    /// Provides the configuration settings for this assembly and current application.
    /// </summary>
    public static class ConfigSettings
    {
        #region Private Constants

        private const string CustomSectionConstant = "customConfiguration";
        private const string CloudServicesSectionConstant = "cloudServices";
        private const string CloudConnectorRoutingAddressConstant = "CloudConnector.Routing.Address";
        private const string PluginsPathConstant = "Plugins.Path";
        private const string PluginsFilterConstant = "Plugins.Filter";

        #endregion

        #region Public Constants

        /// <summary>
        /// Configuration file of the CloudServices.
        /// </summary>
        /// <remarks>
        /// This file is created by the connector initializer in the ERP \\%InstallationDir%\Config\.
        /// </remarks>
        public const string CloudServicesConfigFileConstant = "CloudServices.config";

        #endregion

        #region Private Members

        private static System.Configuration.Configuration currentConfiguration;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current configuration.
        /// </summary>
        /// <value>The current configuration.</value>
        public static System.Configuration.Configuration CurrentConfiguration
        {
            get
            {
                if (currentConfiguration == null)
                {
                    using (var scope = new PerformanceScope("CurrentConfiguration"))
                    {
                        currentConfiguration = LoadConfiguration(ConfigHelper.AssemblyConfigurationFile);                        
                    }
                }

                return currentConfiguration;
            }
        }

        /// <summary>
        /// Gets the Custom configuration section.
        /// </summary>
        /// <value>The cloud services section.</value>
        public static CustomConfigurationSection CustomSection
        {
            get
            {
                return CurrentConfiguration.GetSection<CustomConfigurationSection>(CustomSectionConstant);
            }
        }

        /// <summary>
        /// Gets the CloudServices configuration section.
        /// </summary>
        /// <value>The cloud services section.</value>
        public static CustomConfigurationSection CloudServicesSection
        {
            get
            {
                return CurrentConfiguration.GetSection<CustomConfigurationSection>(CloudServicesSectionConstant);
            }
        }

        /// <summary>
        /// Gets the cloud connector routing address.
        /// </summary>
        /// <value>The cloud connector routing address.</value>
        public static string CloudConnectorRoutingAddress
        {
            get
            {
                return GetCloudServicesSectionElement(CloudConnectorRoutingAddressConstant);
            }
        }

        /// <summary>
        /// Gets the plugins path.
        /// </summary>
        /// <value>The plugins path.</value>
        public static string PluginsPath
        {
            get
            {
                return AssemblyHelper.AssemblyPath + GetCustomSectionElementOrDefault(PluginsPathConstant, ".");
            }
        }

        /// <summary>
        /// Gets the plugins filter.
        /// </summary>
        /// <value>The plugins filter.</value>
        public static string PluginsFilter
        {
            get
            {
                return GetCustomSectionElementOrDefault(PluginsFilterConstant, "*.dll");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="configFileName">Name of the config file.</param>
        /// <returns>The configuration.</returns>
        public static System.Configuration.Configuration LoadConfiguration(string configFileName)
        {
            // TROUBLESHOOTING:
            // When you suspect that ERP is not getting the expected configuration from the app.config,
            // and therefore is not working properly because of WCF communication exceptions and other errors,
            // you must clean the "<ERP>.exe.config" because the configuration file might be outdated.

            try
            {
                // Current configuration of the running application (app.exe)

                System.Configuration.Configuration appConfig = ConfigHelper.LoadConfiguration();

                // Import the file configuration into the current configuration

                if (File.Exists(configFileName))
                {
                    LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "File: {0}", configFileName), "ConfigSettings.LoadConfiguration()");
                    appConfig.ImportConfiguration(configFileName, true);
                }
                else
                {
                    LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "File is Missing: {0}", configFileName), "ConfigSettings.LoadConfiguration()");
                    ConfigurationFileException handledException = new ConfigurationFileException(configFileName);
                    throw handledException;
                }

                // Return current configuration

                return appConfig;
            }
            catch (Exception ex)
            {
                LogHelper.ApplicationLog(ex.ToString(), "ConfigSettings.LoadConfiguration()");
                throw;
            }
        }

        #endregion

        #region Private Methods

        private static CustomConfigurationElement[] GetCustomSectionElements(string sectionName)
        {
            CustomConfigurationSection section = CurrentConfiguration.GetSection<CustomConfigurationSection>(sectionName);
            CustomConfigurationElement[] elements = null;
            section.Dictionary.CopyTo(elements, 0);
            return elements;
        }

        private static string GetCustomSectionElement(string elementName)
        {
            return CustomSection.Dictionary[elementName].Value;
        }

        private static string GetCustomSectionElementOrDefault(string elementName, string elementDefault)
        {
            try
            {
                return GetCustomSectionElement(elementName);
            }
            catch
            {
                return elementDefault;
            }
        }

        private static string GetCloudServicesSectionElement(string elementName)
        {
            return CloudServicesSection.Dictionary[elementName].Value;
        }

        #endregion
    }
}
