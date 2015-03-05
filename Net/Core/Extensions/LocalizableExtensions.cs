using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace BinaryLeaks.Core.Extensions
{
    /// <summary>
    /// Provides localizable extension methods.
    /// </summary>
    public static class LocalizableExtensions
    {
        /// <summary>
        /// Gets the string value with the specified LCID.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="cultureLCID">The culture LCID.</param>
        /// <returns>
        /// Returns the localized string value.
        /// </returns>
        public static string GetString(this string resource, int cultureLCID)
        {
            /* Xml resource sample:

                string str = "<resource>
                                <value lcid="2070">Sim</value>
                                <value lcid="1034">Si</value>
                                <value lcid="3082">Si</value>
                                <value lcid="1033">Yes</value>
                                <value lcid="1036">Ouis</value>
                              </resources>"
            */

            try
            {
                // The resource is a xml content

                XDocument doc = XDocument.Parse(resource);

                // Try to return the resource with the required LCID

                XElement valueElement = doc.Root.Elements().FirstOrDefault(element =>
                    element.Attribute("lcid").Value == cultureLCID.ToString(CultureInfo.InvariantCulture));

                if (valueElement == null)
                {
                    // Try to return the resource with the current thread LCID

                    valueElement = doc.Root.Elements().FirstOrDefault(element =>
                        element.Attribute("lcid").Value == System.Threading.Thread.CurrentThread.CurrentUICulture.LCID.ToString(CultureInfo.InvariantCulture));

                    if (valueElement == null)
                    {
                        valueElement = doc.Root.Elements().First();
                    }
                }

                return valueElement.Value;
            }
            catch (Exception)
            {
                // The resource is a plain text

                return resource;
            }
        }
    }
}
