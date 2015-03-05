using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using BinaryLeaks.Core.Helpers;

namespace BinaryLeaks.Core.Context
{
    /// <summary>
    /// Class exposed to COM. 
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(Guids.ContextDictionary)]
    public class ContextDictionary : OrderedDictionary, IContextDictionary, IDisposable
    {
        #region Private Members

        private bool disposed;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextDictionary"/> class.
        /// </summary>
        public ContextDictionary()
        {
            this.disposed = false;
        }

        #endregion

        #region Destructor

        ~ContextDictionary()
        {
            this.Dispose(false);
        }

        #endregion

        #region Public Properties

        // public IEnumerator _NewEnum
        // {
        //    get
        //    {
        //        return base.GetEnumerator();
        //    }
        // }

        /// <summary>
        /// Gets an array of keys.
        /// </summary>
        /// <value>The array keys.</value>
        public object[] ArrayKeys
        {
            get 
            {
                object[] keys = new object[this.Count];
                this.Keys.CopyTo(keys, 0);
                return keys;                
            }
        }

        /// <summary>
        /// Gets an array of values.
        /// </summary>
        /// <value>The array values.</value>
        public object[] ArrayValues
        {
            get
            {
                object[] values = new object[this.Count];
                this.Values.CopyTo(values, 0);
                return values;
            }
        }

        /// <summary>
        /// Gets an array of items.
        /// </summary>
        /// <value>The array items.</value>
        public object[] ArrayItems
        {
            get
            {
                object[] items = new object[this.Count];
                this.CopyTo(items, 0);
                return items;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Compatibility stub to maintain COM compatibility.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// The response.
        /// </returns>
        public object CompatibilityStub(object request)
        {
            // Add new methods here.
            return null;
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The item object.</returns>
        public object ItemAt(int index)
        {
            return this[index];
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="key">The key object.</param>
        /// <returns>The item of type.</returns>
        public T ItemSearch<T>(object key)
        {
            return this.ItemSearchDefault<T>(key, null);
        }

        /// <summary>
        /// Gets the item with the specified key or default if not found.
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="key">The key object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The item of type.</returns>
        public T ItemSearchDefault<T>(object key, object defaultValue)
        {
            object value = this.ItemSearchDefault(key, defaultValue);

            T typedValue;

            if (!ConvertHelper.TryConvert<T>(value, out typedValue))
            {
                typedValue = (T)defaultValue;
            } 

            return typedValue;
        }

        /// <summary>
        /// Gets the item with the specified key.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>The item object.</returns>
        public object ItemSearch(object key)
        {
            return this.ItemSearchDefault(key, null);
        }

        /// <summary>
        /// Gets the item with the specified key or default if not found.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The item object.</returns>
        public object ItemSearchDefault(object key, object defaultValue)
        {
            int indexKey = -1;
            if (int.TryParse(key.ToString(), out indexKey))
            {
                // Search key as integer index

                if (indexKey < this.Keys.Count)
                {
                    return this[indexKey];
                }
                else
                {
                    return defaultValue;
                }
            }
            else
            {
                // Search key as case insensitive string

                object caseInsensitiveKey = this.ArrayKeys.FirstOrDefault(p => ((string)p).ToUpper(CultureInfo.InvariantCulture) == ((string)key).ToUpper(CultureInfo.InvariantCulture));

                if (caseInsensitiveKey != null)
                {
                    return this[caseInsensitiveKey];
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Updates the specified key value.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="value">The value.</param>
        public void Update(object key, object value)
        {
            this[key] = value;
        }

        /// <summary>
        /// Updates the value of the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        public void UpdateAt(int index, object value)
        {
            this[index] = value;
        }

        /// <summary>
        /// Parses the key as enumeration string or object.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>The string key.</returns>
        /// <remarks>
        /// Enumeration values exposed to COM are handled as integers.
        /// </remarks>
        public object KeyParse(object key)
        {
            // Parse key as enum string

            if (key.GetType().IsEnum)
            {
                return this.KeyParse((Enum)key);
            }

            // Parse key as object (string or integer index)

            return key;
        }

        /// <summary>
        /// Parses the enumeration key value into a string key.
        /// </summary>
        /// <param name="enumValue">The enumeration value.</param>
        /// <returns>The string key.</returns>
        /// <remarks>
        /// Enumeration values exposed to COM are handled as integers.
        /// </remarks>
        public string KeyParse(int enumValue)
        {
            // Returns the value of custom attribute 'StringValueAttribute'
            // for the specified enumerator value

            if (Enum.IsDefined(typeof(Constants.ContextKey), enumValue))
            {
                return this.KeyParse((Enum)Enum.ToObject(typeof(Constants.ContextKey), enumValue));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Parses the enumeration key field into a string key.
        /// </summary>
        /// <param name="enumField">The enumeration field.</param>
        /// <returns>The string key.</returns>
        /// <remarks>
        /// Enumeration values exposed to COM are handled as integers.
        /// </remarks>
        public string KeyParse(Enum enumField)
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

        #region IDisposable Members

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">Returns <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called

            if (!this.disposed)
            {
                // Dispose managed resources

                if (disposing)
                {
                    for (int i = 0; i < this.Count - 1; i++)
                    {
                        IDisposable item = this[i] as IDisposable;

                        if (item != null)
                        {
                            item.Dispose();
                        }

                        this[i] = null;
                    }

                    this.Clear();
                }

                // Dispose unmanaged resources

                // do nothing

                // Force GC

                GC.Collect();

                // Update disposing state

                this.disposed = true;
            }
        }

        #endregion
    }
}
