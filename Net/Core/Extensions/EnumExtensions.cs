using System;
using System.Reflection;
using BinaryLeaks.Core.Context;

namespace BinaryLeaks.Core.Extensions
{
    /// <summary>
    ///   <see cref="EnumExtensions"/> class.
    /// </summary>
    public static class EnumExtensions
    {
        #region Enum Extensions Methods

        /// <summary>
        /// Parses an enumeration key field into a string key.
        /// </summary>
        /// <param name="enumField">The enumeration field.</param>
        /// <returns>The string key.</returns>
        /// <remarks>
        /// Enumeration values exposed to COM are handled as integers.
        /// </remarks>
        public static string GetString(this Enum enumField)
        {
            // Returns the value of custom attribute 'StringValueAttribute'
            // for the specified enumerator field

            FieldInfo enumInfo = enumField.GetType().GetField(enumField.ToString());
            StringValueAttribute[] attrs = enumInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            if (attrs.Length > 0)
            {
                return attrs[0].Value;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
