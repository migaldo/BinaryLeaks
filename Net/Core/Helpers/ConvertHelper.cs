using System;
using System.ComponentModel;
using System.Globalization;

namespace Primavera.Platform.CloudServices900.Helpers
{
    /// <summary>
    /// Convert helper class.
    /// </summary>
    public static class ConvertHelper
    {
        #region Public Methods

        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <typeparam name="T">Expected type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted value as T.</returns>
        public static T Convert<T>(object value, T defaultValue)
        {
            T outValue = defaultValue;
            ConvertHelper.TryConvert<T>(value, out outValue);
            return outValue;
        }

        /// <summary>
        /// Tries the convert.
        /// </summary>
        /// <typeparam name="T">Expected type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="outValue">The out value.</param>
        /// <returns>The converted value as T.</returns>
        public static bool TryConvert<T>(object value, out T outValue)
        {
            // Identity types

            if (value != null && value is T)
            {
                outValue = (T)value;
                return true;
            }

            // Class types

            if (value == null && typeof(T).IsClass)
            {
                outValue = default(T);
                return true;
            }

            // Enum types

            if (value != null && typeof(T).IsEnum)
            {
                outValue = (T)Enum.Parse(typeof(T), value.ToString());
                return true;
            }

            // Compatible reference or nullable types

            var convertible = value as IConvertible;

            if (convertible != null && ConvertibleHandlesDestinationType<T>())
            {
                outValue = (T)System.Convert.ChangeType(convertible, typeof(T), CultureInfo.CurrentCulture);
                return true;
            }

            // Other types

            if (value != null)
            {
                // Assigned types

                if (typeof(T).IsAssignableFrom(value.GetType()))
                {
                    outValue = (T)value;
                    return true;
                }

                // Converter types

                var converter = TypeDescriptor.GetConverter(value);
                if (converter.CanConvertTo(typeof(T)))
                {
                    outValue = (T)converter.ConvertTo(value, typeof(T));
                    return true;
                }
            }

            // Default types

            outValue = default(T);
            return false;
        }

        /// <summary>
        /// Gets the value or default.
        /// </summary>
        /// <typeparam name="T">The <c>T</c> type.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The <c>T</c> value.</returns>
        public static T GetValueOrDefault<T>(Func<T> function, T defaultValue)
        {
            try
            {
                return function();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        #endregion

        #region Private Methods
        
        private static bool ConvertibleHandlesDestinationType<T>()
        {
            return
                typeof(T).Equals(typeof(bool)) ||
                typeof(T).Equals(typeof(byte)) ||
                typeof(T).Equals(typeof(char)) ||
                typeof(T).Equals(typeof(DateTime)) ||
                typeof(T).Equals(typeof(decimal)) ||
                typeof(T).Equals(typeof(double)) ||
                typeof(T).Equals(typeof(short)) ||
                typeof(T).Equals(typeof(int)) ||
                typeof(T).Equals(typeof(long)) ||
                typeof(T).Equals(typeof(sbyte)) ||
                typeof(T).Equals(typeof(float)) ||
                typeof(T).Equals(typeof(string)) ||
                typeof(T).Equals(typeof(ushort)) ||
                typeof(T).Equals(typeof(uint)) ||
                typeof(T).Equals(typeof(ulong));
        }

        #endregion
    } 
}
