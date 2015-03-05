using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace BinaryLeaks.Core.Configuration
{
    /// <summary>
    /// Configuration file exception class.
    /// </summary>
    [Serializable]
    public class ConfigurationFileException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationFileException"/> class.
        /// </summary>
        public ConfigurationFileException()
        {
            // Add any type-specific logic, and supply the default message.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationFileException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        protected ConfigurationFileException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Implement type-specific serialization constructor logic.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationFileException"/> class.
        /// </summary>
        /// <param name="configFileName">Name of the config file.</param>
        public ConfigurationFileException(string configFileName)
            : base(string.Format(CultureInfo.CurrentCulture, Properties.Resources.RES_ConfigurationFileException, configFileName))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationFileException"/> class.
        /// </summary>
        /// <param name="configFileName">Name of the config file.</param>
        /// <param name="innerException">The inner exception.</param>
        public ConfigurationFileException(string configFileName, Exception innerException)
            : base(string.Format(CultureInfo.CurrentCulture, Properties.Resources.RES_ConfigurationFileException, configFileName), innerException)
        {
        }

        #endregion
    }
}
