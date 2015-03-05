using System;
using System.Runtime.InteropServices;

namespace Primavera.Platform.CloudServices900.Context
{
    /// <summary>
    /// Interface exposed to COM.
    /// </summary>
    [ComVisible(true)]
    [Guid(Guids.IContextDictionary)]
    public interface IContextDictionary
    {
        /// <summary>
        /// Compatibility stub to maintain COM compatibility.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        object CompatibilityStub(object request);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        void Dispose();

        // If you're using ForEach, you'll need to implement the _NewEnum property and
        // a kind of IEnumerator interface - COM friendly strong typed GetEnumerator
        // [DispId(-4)]
        // IEnumerator _NewEnum { get; }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified key.
        /// </summary>
        /// <param name="key">The key of the item.</param>
        /// <value>The item with the specified key.</value>
        /// <returns>The item specified.</returns>
        object this[object key] { get; set; }

        // Only one indexer type can be exposed to COM
        // object this[int index] { get; set; }

        /// <summary>
        /// Gets the count of items.
        /// </summary>
        /// <value>The count.</value>
        int Count { get; }

        /// <summary>
        /// Gets an array of keys.
        /// </summary>
        /// <value>The array keys.</value>
        object[] ArrayKeys { get; }

        /// <summary>
        /// Gets an array of values.
        /// </summary>
        /// <value>The array values.</value>
        object[] ArrayValues { get; }

        /// <summary>
        /// Gets an array of items.
        /// </summary>
        /// <value>The array items.</value>
        object[] ArrayItems { get; }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The item object.</returns>
        object ItemAt(int index);

        /// <summary>
        /// Gets the item with the specified key.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>The item object.</returns>
        object ItemSearch(object key);

        // Don't expose generic methods to COM
        // T ItemSearch<T>(object key, object defaultValue = null);

        /// <summary>
        /// Gets the item with the specified key or default if not found.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The item object.</returns>
        object ItemSearchDefault(object key, object defaultValue);

        /// <summary>
        /// Adds the specified key value.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="value">The value.</param>
        void Add(object key, object value);

        /// <summary>
        /// Updates the specified key value.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="value">The value.</param>
        void Update(object key, object value);

        /// <summary>
        /// Updates the value of the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        void UpdateAt(int index, object value);

        /// <summary>
        /// Removes the item with specified key.
        /// </summary>
        /// <param name="key">The key object.</param>
        void Remove(object key);

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        void RemoveAt(int index);

        /// <summary>
        /// Determines whether [contains] [the specified key].
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>
        /// Returns <c>true</c> if [contains] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(object key);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();

        /// <summary>
        /// Parses the enumeration key into a string key.
        /// </summary>
        /// <remarks>
        /// Enumeration values exposed to COM are handled as integers.
        /// </remarks>
        /// <param name="enumValue">The enumeration value.</param>
        /// <returns>The item object.</returns>
        string KeyParse(int enumValue);
    }
}
