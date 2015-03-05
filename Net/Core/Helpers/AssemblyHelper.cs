using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace BinaryLeaks.Core.Helpers
{
    /// <summary>
    /// Provides the Assembly helper.
    /// </summary>
    public static class AssemblyHelper
    {
        #region Static Members

        private static bool attached = false;
        private static bool loaded = false;

        #endregion

        #region  Public Static Properties

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <returns></returns>
        public static string ApplicationName
        {
            get
            {
                // A COM application that uses this assembly through TypeLib returns null GetEntryAssembly
                // Alternative code: return System.Reflection.Assembly.GetEntryAssembly().GetName().FullName;

                return AppDomain.CurrentDomain.SetupInformation.ApplicationName;
            }
        }

        /// <summary>
        /// Gets the path of application.
        /// </summary>
        /// <returns></returns>
        public static string ApplicationPath
        {
            get
            {
                // A COM application that uses this assembly through TypeLib returns null GetEntryAssembly
                // Alternative code: return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                // Alternative code: return Environment.GetCommandLineArgs()[0];

                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
        }

        /// <summary>
        /// Gets the path of current assembly.
        /// </summary>
        /// <returns></returns>
        public static string AssemblyPath
        {
            get
            {
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        /// <summary>
        /// Gets the name of the assembly file.
        /// </summary>
        /// <returns></returns>
        public static string AssemblyFileName
        {
            get
            {
                return System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        /// <summary>
        /// Gets the name of the assembly.
        /// </summary>
        /// <returns></returns>
        public static string AssemblyName
        {
            get
            {
                return System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().FullName);
            }
        }

        /// <summary>
        /// Gets the temporary path.
        /// </summary>
        /// <returns></returns>
        public static string TempPath
        {
            get
            {
                string path = System.Environment.GetEnvironmentVariable("TEMP");

                if (!path.EndsWith("\\", StringComparison.OrdinalIgnoreCase))
                {
                    path += "\\";
                }

                return path;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Attach global event handlers to current application domain
        /// in the static constructor, this is before any instance constructor.
        /// </summary>
        /// <remarks>
        /// 1. Attach the resolve event handler in the static constructor, before any instance constructor.
        /// Whenever a referenced assembly is not in the same path of executing application try to resolve it manually.
        /// Assemblies exposed to COM don't need this technique because they are registered as <c>typelibs</c>,
        /// but their referenced assemblies need the resolver.
        /// 2. Attach the unhandled exception event handler.
        /// </remarks>
        public static void AttachCurrentDomainEventHandlers()
        {
            // The AssemblyResolve event is called when the common language runtime
            // tries to bind to the assembly and fails

            // Only once per application
            if (!attached)
            {
                AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

                //TESTES CRASH
                //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                attached = true;
            }
        }

        /// <summary>
        /// Detach global event handlers from current application domain.
        /// </summary>
        public static void DettachCurrentDomainEventHandlers()
        {
            if (attached)
            {
                AppDomain.CurrentDomain.AssemblyLoad -= new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
                AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(CurrentDomain_AssemblyResolve);

                //TESTES CRASH
                //AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                attached = false;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the AssemblyLoad event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="System.AssemblyLoadEventArgs"/> instance containing the event data.</param>
        private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            // Only once per application

            if (!loaded)
            {
                // Enabel application visual styles
                // (required by forms and controls to use windows themes)

                Application.EnableVisualStyles();

                // WARNING - Onde podemos colocar o Application.SetCompatibleTextRenderingDefault ?
                //// Application.SetCompatibleTextRenderingDefault(false);

                loaded = true;
            }
        }

        /// <summary>
        /// Handles the UnhandledException event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Do not use ExceptionsHandler or LogHandler here because the exception could have been raised
            // by that componentes. Instead you must use a simple logging mechanism...

            if (e.ExceptionObject != null)
            {
                Exception ex = (Exception)e.ExceptionObject;
                LogHelper.ApplicationLog(ex.ToString(), "AssemblyHelper.CurrentDomain_UnhandledException()", LogHelper.LogEventType.Default);
                LogHelper.ApplicationLog(ex.ToString(), "AssemblyHelper.CurrentDomain_UnhandledException()", LogHelper.LogEventType.Verbose);
                throw ex;
            }
        }

        /// <summary>
        /// Handles the AssemblyResolve event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="System.ResolveEventArgs"/> instance containing the event data.</param>
        /// <returns>The resolved assembly or null.</returns>
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // This handler is called only when the common language runtime
            // tries to bind to the assembly and fails

            Assembly assembly = null;

            // Get the assembly name that raised the resolve event

            string missingAssemblyName = ParseAssemblyName(args.Name);

            // Get the path of executing assembly to search for the missing assembly

            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            string assembliesPath = Path.GetDirectoryName(executingAssembly.Location);
            string missingAssemblyFile = Path.Combine(assembliesPath, missingAssemblyName + ".dll");

            // Check if it belongs to the referenced assemblies (optional checking)

            /*
            AssemblyName[] referencedAssemblies = executingAssembly.GetReferencedAssemblies();
            AssemblyName referencedAssemblyName = referencedAssemblies.FirstOrDefault<AssemblyName>(a => ParseAssemblyName(a.FullName) == missingAssemblyName);

            if (referencedAssemblyName == null)
            {
                return assembly;
            }
            */

            // Try to load missing assembly from the same path of executing assembly

            if (File.Exists(missingAssemblyFile))
            {
                try
                {
                    assembly = Assembly.LoadFrom(missingAssemblyFile);
                    LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "Resolved: {0}", missingAssemblyFile), "AssemblyHelper.CurrentDomain_AssemnlyResolve()");
                }
                catch (Exception ex)
                {
                    LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "Loading exception: {0}", ex.ToString()), "AssemblyHelper.CurrentDomain_AssemnlyResolve()");
                }
            }
            else
            {
                LogHelper.ApplicationLog(string.Format(CultureInfo.InvariantCulture, "Failed to resolve: {0}", missingAssemblyFile), "AssemblyHelper.CurrentDomain_AssemnlyResolve()");
            }

            // Return loaded assembly 
            // or null if custom resolver also failed

            return assembly;
        }

        /// <summary>
        /// Parses the name of the assembly.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <returns>The parsed name of the assembly.</returns>
        private static string ParseAssemblyName(string fullName)
        {
            int pos = fullName.IndexOf(",", StringComparison.OrdinalIgnoreCase);

            if (pos > 0)
            {
                return fullName.Substring(0, pos);
            }
            else
            {
                return fullName;
            }
        }

        #endregion
    }
}
