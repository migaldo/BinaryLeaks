using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Primavera.Platform.CloudServices900.Configuration
{
    /// <summary>
    /// Configuration not found exception class.
    /// </summary>
    [Serializable]
    public class ConfigurationNotFoundException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationNotFoundException"/> class.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        public ConfigurationNotFoundException(string elementName)
            : base(string.Format(CultureInfo.CurrentCulture, Properties.Resources.RES_ConfigurationNotFoundException, elementName))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationNotFoundException"/> class.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="innerException">The inner exception.</param>
        public ConfigurationNotFoundException(string elementName, Exception innerException)
            : base(string.Format(CultureInfo.CurrentCulture, Properties.Resources.RES_ConfigurationNotFoundException, elementName), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationNotFoundException"/> class.
        /// </summary>
        public ConfigurationNotFoundException()
        {
            // Add any type-specific logic, and supply the default message.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationNotFoundException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        protected ConfigurationNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Implement type-specific serialization constructor logic.
        }

        #endregion
    }
}
