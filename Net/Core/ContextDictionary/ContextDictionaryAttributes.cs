using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Primavera.Platform.CloudServices900.Context
{
    /// <summary>
    /// Class that implements the StringValueAttribute.
    /// </summary>
    /// <remarks>
    /// Use this attribute to assign a string value to enumerated items.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class StringValueAttribute : Attribute
    {
        private string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringValueAttribute"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public StringValueAttribute(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                return this.value; 
            }
        }
    }    
}
