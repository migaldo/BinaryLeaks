using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using Primavera.Platform.CloudServices900.Helpers;

namespace Primavera.Platform.CloudServices900.Configuration
{
    /// <summary>
    /// Provides a Configuration helper.
    /// </summary>
    public static class ConfigHelper
    {
        #region Public Properties

        /// <summary>
        /// Gets the application configuration file (app.exe.config).
        /// </summary>
        /// <value>The application configuration file.</value>
        public static string ApplicationConfigurationFile
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

                /* Alternative code:
                return System.IO.Path.Combine(
                    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location),
                    System.IO.Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location) + ".config");
                */
            }
        }

        /// <summary>
        /// Gets the assembly configuration file.
        /// </summary>
        /// <value>The assembly configuration file.</value>
        public static string AssemblyConfigurationFile
        {
            get
            {
                return System.IO.Path.Combine(
                    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location) + ".config");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the configuration of current application from the default configuration file (app.exe.config).
        /// </summary>
        /// <returns>The current configuration.</returns>
        public static System.Configuration.Configuration LoadConfiguration()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config == null)
            {
                throw new ConfigurationInvalidException();
            }

            // Debug info: config.SaveAs("debug.config")
            return config;
        }

        /// <summary>
        /// Loads the configuration from the specified configuration file.
        /// </summary>
        /// <param name="configFileName">Configuration file name.</param>
        /// <returns>The file configuration.</returns>
        public static System.Configuration.Configuration LoadConfiguration(string configFileName)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = configFileName;

            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            if (config == null)
            {
                throw new ConfigurationInvalidException();
            }

            // Debug info: config.SaveAs("debug.config")
            return config;
        }

        #endregion

        #region Extension Methods

        /// <summary>
        /// Gets the specified section from configuration.
        /// </summary>
        /// <typeparam name="TConfigurationSection">The type of the configuration section.</typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>The TConfigurationSection.</returns>
        public static TConfigurationSection GetSection<TConfigurationSection>(this System.Configuration.Configuration config, string sectionName)
                where TConfigurationSection : ConfigurationSection
        {
            TConfigurationSection section = config.TryGetSection<TConfigurationSection>(sectionName);

            if (section == null)
            {
                throw new ConfigurationNotFoundException(sectionName);
            }

            // Debug info: section.SectionInformation.GetRawXml()            
            return section;
        }

        /// <summary>
        /// Tries to get the specified section from configuration.
        /// </summary>
        /// <typeparam name="TConfigurationSection">The type of the configuration section.</typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>The TConfigurationSection.</returns>
        public static TConfigurationSection TryGetSection<TConfigurationSection>(this System.Configuration.Configuration config, string sectionName)
                where TConfigurationSection : ConfigurationSection
        {
            return config.GetSection(sectionName) as TConfigurationSection;
        }

        /// <summary>
        /// Gets the specified section group from configuration.
        /// </summary>
        /// <typeparam name="TConfigurationSectionGroup">The type of the configuration section group.</typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="sectionGroupName">Name of the section group.</param>
        /// <returns>The TConfigurationSectionGroup.</returns>
        public static TConfigurationSectionGroup GetSectionGroup<TConfigurationSectionGroup>(this System.Configuration.Configuration config, string sectionGroupName)
                where TConfigurationSectionGroup : ConfigurationSectionGroup
        {
            TConfigurationSectionGroup sectionGroup = config.GetSectionGroup(sectionGroupName) as TConfigurationSectionGroup;

            if (sectionGroup == null)
            {
                throw new ConfigurationNotFoundException(sectionGroupName);
            }

            return sectionGroup;
        }

        /// <summary>
        /// Copies the source configuration section into the target configuration section.
        /// </summary>
        /// <typeparam name="TConfigurationSection">The type of the configuration section.</typeparam>
        /// <param name="source">The source configuration.</param>
        /// <param name="target">The target configuration.</param>
        public static void CopyTo<TConfigurationSection>(this TConfigurationSection source, TConfigurationSection target)
            where TConfigurationSection : ConfigurationSection
        {
            target.SectionInformation.SetRawXml(source.SectionInformation.GetRawXml());
            ConfigurationManager.RefreshSection(target.SectionInformation.Name);
        }

        /// <summary>
        /// Clones the configuration section into a new reflected instance.
        /// </summary>
        /// <typeparam name="TConfigurationSection">The type of the configuration section.</typeparam>
        /// <param name="source">The source configuration.</param>
        /// <returns>The TConfigurationSection.</returns>
        public static TConfigurationSection Clone<TConfigurationSection>(this TConfigurationSection source)
            where TConfigurationSection : ConfigurationSection
        {
            TConfigurationSection reflected = Activator.CreateInstance(source.GetType()) as TConfigurationSection;            
            reflected.SectionInformation.SetRawXml(source.SectionInformation.GetRawXml());
            reflected.SectionInformation.Type = source.SectionInformation.Type;
            return reflected;
        }

        /// <summary>
        /// Imports the section from source configuration.
        /// </summary>
        /// <typeparam name="TConfigurationSection">The type of the configuration section.</typeparam>
        /// <param name="target">The target configuration.</param>
        /// <param name="source">The source configuration.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="sectionOverride">If set to <c>true</c> [section override].</param>
        /// <returns>Returns the added section.</returns>
        public static TConfigurationSection ImportSection<TConfigurationSection>(this System.Configuration.Configuration target, System.Configuration.Configuration source, string sectionName, bool sectionOverride)
            where TConfigurationSection : ConfigurationSection
        {
            Debug.WriteLine(sectionName);

            // Source section

            TConfigurationSection sourceSection = source.TryGetSection<TConfigurationSection>(sectionName);

            if (sourceSection == null)
            {
                return null;
            }

            // Import source section into target configuration

            return target.ImportSection<TConfigurationSection>(sourceSection, sectionOverride);
        }

        /// <summary>
        /// Imports the section from source configuration.
        /// </summary>
        /// <typeparam name="TConfigurationSection">The type of the configuration section.</typeparam>
        /// <param name="target">The target configuration.</param>
        /// <param name="sourceSection">The source configuration section.</param>
        /// <param name="sectionOverride">If set to <c>true</c> [section override].</param>
        /// <returns>Returns the imported section.</returns>
        public static TConfigurationSection ImportSection<TConfigurationSection>(this System.Configuration.Configuration target, TConfigurationSection sourceSection, bool sectionOverride)
            where TConfigurationSection : ConfigurationSection
        {
            // Section name

            string sectionName = sourceSection.SectionInformation.SectionName;

            // Target section exists?

            TConfigurationSection targetSection = target.TryGetSection<TConfigurationSection>(sectionName);

            if (targetSection != null)
            {
                // Override target section?

                if (sectionOverride)
                {
                    if (!targetSection.Equals(sourceSection))
                    {
                        LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "Section (Override): {0}", sectionName), "ConfigHelper.ImportSection<T>()");

                        string sourceSectionXml = sourceSection.SectionInformation.GetRawXml();
                        targetSection.SectionInformation.SetRawXml(sourceSectionXml);
                    }
                    else
                    {
                        LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "Section (Equal): {0}", sectionName), "ConfigHelper.ImportSection<T>()");
                        return null;
                    }
                }
                else
                {
                    LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "Section (Ignore): {0}", sectionName), "ConfigHelper.ImportSection<T>()");
                    return null;
                }
            }
            else
            {
                LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "Section (New): {0}", sectionName), "ConfigHelper.ImportSection<T>()");

                targetSection = sourceSection.Clone();
                target.Sections.Add(sectionName, targetSection);
            }

            return targetSection;
        }

        /// <summary>
        /// Imports the configuration file into the target configuration using runtime injection.
        /// </summary>
        /// <param name="target">The target configuration.</param>
        /// <param name="configFileName">The configuration file name.</param>
        /// <param name="sectionOverride">If set to <c>true</c> [section override].</param>
        /// <returns>The configuration.</returns>
        public static System.Configuration.Configuration ImportConfiguration(this System.Configuration.Configuration target, string configFileName, bool sectionOverride)
        {
            try
            {
                // Collection of imported sections to refresh later

                Collection<string> refeshSections = new Collection<string>();

                // Open the configuration file

                System.Configuration.Configuration fileConfig = LoadConfiguration(configFileName);

                // Import the configuration file into the target configuration,
                // using runtime injection, and overriding existing sections

                foreach (ConfigurationSection section in fileConfig.Sections)
                {
                    Debug.WriteLine(section.SectionInformation.SectionName);

                    // Skip empty sections

                    if (!string.IsNullOrEmpty(section.SectionInformation.GetRawXml()))
                    {
                        // Imported section

                        ConfigurationSection importedSection = target.ImportSection<ConfigurationSection>(section, sectionOverride);

                        if (importedSection != null)
                        {
                            importedSection.SectionInformation.ForceSave = true;
                            refeshSections.Add(importedSection.SectionInformation.SectionName);
                        }
                    }
                }

                // IMPORTANT NOTE: 
                // Save the current configuration to file to force the configuration reload and refresh (mandatory)
                // Look for the file "apl.exe.config" in the current application's path - it has to be updated!
                // Avoid the ConfigurationSaveMode.Full because it could generate duplicated section errors.

                target.Save(ConfigurationSaveMode.Modified, true);

                // Force the reload of imported sections. 
                // This makes the new values available for reading.

                foreach (string sectionName in refeshSections)
                {
                    ConfigurationManager.RefreshSection(sectionName);
                }

                // Return current configuration

                return target;
            }
            catch (Exception ex)
            {
                ConfigurationFileException handledException = new ConfigurationFileException(configFileName, ex);
                LogHelper.ApplicationLog(handledException.ToString(), "ConfigHelper.ImportConfiguration()");

                throw handledException;
            }
        }

        #endregion
    }
}
